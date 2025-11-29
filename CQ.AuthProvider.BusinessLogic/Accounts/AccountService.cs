using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.Utility;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Accounts;

internal sealed class AccountService(
    IAccountRepository accountRepository,
    IIdentityRepository identityRepository,
    ISessionInternalService _sessionService,
    IRoleRepository _roleRepository,
    IAppInternalService _appService,
    ITenantRepository tenantRepository,
    IUnitOfWork unitOfWork)
    : IAccountInternalService
{
    #region Create
    public async Task<CreateAccountResult> CreateIdentityAndSaveAsync(
        Account account,
        string password,
        bool passwordIsHash = false)
    {
        var identity = new Identity
        {
            Id = account.Id,
            Email = account.Email,
            Password = password
        };

        await identityRepository
            .CreateAndSaveAsync(identity, passwordIsHash)
            .ConfigureAwait(false);

        await accountRepository
            .CreateAsync(account)
            .ConfigureAwait(false);

        var session = await _sessionService
            .CreateAsync(
            account,
            account.Apps[0])
            .ConfigureAwait(false);

        var result = new CreateAccountResult(
            account.Id,
            account.Email,
            account.FullName,
            account.FirstName,
            account.LastName,
            account.ProfilePictureKey,
            account.Locale,
            account.TimeZone,
            account.Apps[0],
            session.Token,
            account.Roles.ConvertAll(r => r.Name),
            account.Roles.SelectMany(r => r.Permissions.ConvertAll(p => p.Key)).ToList(),
            account.Tenant);

        return result;
    }

    public async Task<CreateAccountResult> CreateAndSaveAsync(CreateAccountArgs args)
    {
        await AssertExistenseOfEmailAsync(args.Email).ConfigureAwait(false);

        var app = await _appService
            .GetByIdAsync(args.AppId)
            .ConfigureAwait(false);

        var role = await _roleRepository
                .GetDefaultByTenantIdAsync(app.Id, app.Tenant.Id)
                .ConfigureAwait(false);

        var account = Account.New(
            args.Email,
            args.FirstName,
            args.LastName,
            args.ProfilePictureKey,
            args.Locale,
            args.TimeZone,
            role,
            app);

        return await CreateAccountAsync(
            account,
            args.Password,
            args.IsPasswordHashed)
            .ConfigureAwait(false);
    }

    private async Task<CreateAccountResult> CreateAccountAsync(
        Account account,
        string password,
        bool passwordIsHash)
    {
        var result = await CreateIdentityAndSaveAsync(
            account,
            password,
            passwordIsHash)
            .ConfigureAwait(false);

        try
        {
            await unitOfWork
                .CommitChangesAsync()
                .ConfigureAwait(false);
        }
        catch (Exception)
        {
            await identityRepository
                .DeleteByIdAsync(account.Id)
                .ConfigureAwait(false);

            throw;
        }

        return result;
    }

    private async Task AssertExistenseOfEmailAsync(string email)
    {
        var existAuth = await accountRepository
            .ExistByEmailAsync(email)
            .ConfigureAwait(false);
        if (existAuth)
        {
            throw new InvalidOperationException($"Email ({email}) is in use");
        }
    }

    public async Task<Account> CreateAndSaveAsync(
        CreateAccountForArgs args,
        AccountLogged accountLogged)
    {
        await AssertExistenseOfEmailAsync(args.Email).ConfigureAwait(false);

        List<Guid> appIds = [accountLogged.AppLogged.Id];
        if (Guard.IsNotNull(args.AppIds) && args.AppIds.Count > 0)
        {
            appIds = args.AppIds;
        }

        var apps = await _appService
            .GetByIdAsync(appIds)
            .ConfigureAwait(false);

        var roles = await _roleRepository
            .GetByIdAsync(args.RoleIds, appIds, accountLogged.Tenant.Id)
            .ConfigureAwait(false);

        var account = Account.New(
            args.Email,
            args.FirstName,
            args.LastName,
            args.ProfilePictureKey,
            args.Locale,
            args.TimeZone,
            roles,
            apps,
            accountLogged.Tenant);

        var identity = Identity.NewForAccount(account, args.Password);

        await identityRepository
            .CreateAndSaveAsync(identity)
            .ConfigureAwait(false);

        await accountRepository
            .CreateAsync(account)
            .ConfigureAwait(false);

        try
        {
            await unitOfWork
                .CommitChangesAsync()
                .ConfigureAwait(false);
        }
        catch (Exception)
        {
            await identityRepository
                .DeleteByIdAsync(account.Id)
                .ConfigureAwait(false);

            throw;
        }

        await TransferTenantAndRemoveSeedAccountAsync(
            account,
            accountLogged)
            .ConfigureAwait(false);

        return account;
    }

    private async Task TransferTenantAndRemoveSeedAccountAsync(
        Account newAccount,
        AccountLogged accountLogged)
    {
        if (accountLogged.Id != AuthConstants.SEED_ACCOUNT_ID)
        {
            return;
        }

        if (!newAccount.HasPermission(AuthConstants.TENANT_OWNER_ROLE_ID))
        {
            await accountRepository
                .AddRoleByIdAsync(newAccount.Id, AuthConstants.TENANT_OWNER_ROLE_ID)
                .ConfigureAwait(false);
        }

        await accountRepository
            .RemoveRoleByIdAsync
            (AuthConstants.SEED_ACCOUNT_ID,
            AuthConstants.TENANT_OWNER_ROLE_ID)
            .ConfigureAwait(false);

        await identityRepository
            .DeleteByIdAsync(AuthConstants.SEED_ACCOUNT_ID)
            .ConfigureAwait(false);

        await _roleRepository
            .DeleteAndSaveByIdAsync(AuthConstants.SEED_ROLE_ID)
            .ConfigureAwait(false);

        await accountRepository
            .DeleteAndSaveByIdAsync(AuthConstants.SEED_ACCOUNT_ID)
            .ConfigureAwait(false);
    }

    public async Task<CreateAccountResult> CreateAndSaveWithTenantAsync(CreateAccountWithTenantArgs args)
    {
        await AssertExistenseOfEmailAsync(args.Email).ConfigureAwait(false);

        var tenant = new Tenant
        {
            Name = args.TenantName
        };

        await tenantRepository
        .CreateAsync(tenant)
        .ConfigureAwait(false);

        var account = Account.NewWithTenant(
            args.Email,
            args.FirstName,
            args.LastName,
            args.ProfilePictureKey,
            args.Locale,
            args.TimeZone,
            tenant);

        try
        {
            var result = await CreateIdentityAndSaveAsync(
                account,
                args.Password)
                .ConfigureAwait(false);

            await unitOfWork
                .CommitChangesAsync()
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception)
        {
            await identityRepository
                .DeleteByIdAsync(account.Id)
                .ConfigureAwait(false);

            throw;
        }
    }
    #endregion

    public async Task UpdatePasswordAsync(
        UpdatePasswordArgs args,
        AccountLogged accountLogged)
    {
        await identityRepository
            .UpdatePasswordByIdAsync(
            accountLogged.Id,
            args.OldPassword,
            args.NewPassword)
            .ConfigureAwait(false);
    }

    public async Task AssertByEmailAsync(string email)
    {
        var existAccount = await accountRepository
            .ExistByEmailAsync(email)
            .ConfigureAwait(false);

        if (existAccount)
        {
            throw new InvalidOperationException($"Email {email} is used");
        }
    }

    public async Task<Pagination<Account>> GetAllAsync(
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var accounts = await accountRepository
            .GetAllAsync(accountLogged.Tenant.Id, page, pageSize)
            .ConfigureAwait(false);

        return accounts;
    }

    public async Task UpdateRolesAsync(
        Guid id,
        UpdateRolesArgs args,
        AccountLogged accountLogged)
    {
        var account = await accountRepository
            .GetByIdAsync(id, accountLogged)
            .ConfigureAwait(false);

        var rolesToDelete = account
            .Roles
            .Where(r => !args.RoleIds.Contains(r.Id))
            .ToList();
        if (rolesToDelete.Count != 0)
        {
            var roleIdsToDelete = rolesToDelete
                .Select(r => r.Id)
                .ToList();

            await accountRepository
                .DeleteRolesByIdAsync(roleIdsToDelete, account)
                .ConfigureAwait(false);
        }

        var newRoles = args
            .RoleIds
            .Where(ri => !account.Roles.Exists(r => r.Id == ri))
            .ToList();
        if (newRoles.Count != 0)
        {
            var roles = await _roleRepository
                .GetAllByIdsAsync(newRoles, accountLogged)
                .ConfigureAwait(false);

            if (roles.Count != newRoles.Count)
            {
                throw new InvalidOperationException("Some roles don't belong to tenant");
            }

            var rolesNotInApps = roles
                .Where(r => !account.Apps.Exists(a => a.Id == r.AppId))
                .ToList();
            if (rolesNotInApps.Count != 0)
            {
                throw new InvalidOperationException("Some roles don't belong to apps of account");
            }

            await accountRepository
                .AddRolesByIdAsync(newRoles, account)
                .ConfigureAwait(false);
        }

        await unitOfWork
            .CommitChangesAsync()
            .ConfigureAwait(false);
    }
}
