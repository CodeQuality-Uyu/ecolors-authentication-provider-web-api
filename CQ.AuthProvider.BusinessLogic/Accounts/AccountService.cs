using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.Utility;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Accounts;

internal sealed class AccountService(
    IAccountRepository _accountRepository,
    IIdentityRepository _identityRepository,
    ISessionInternalService _sessionService,
    IRoleRepository _roleRepository,
    IAppInternalService _appService,
    IUnitOfWork _unitOfWork)
    : IAccountInternalService
{
    #region Create
    public async Task<CreateAccountResult> CreateIdentityAndSaveAsync(
        Account account,
        string password)
    {
        var identity = new Identity
        {
            Id = account.Id,
            Email = account.Email,
            Password = password
        };

        await _identityRepository
            .CreateAndSaveAsync(identity)
            .ConfigureAwait(false);

        await _accountRepository
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
            account.ProfilePictureId,
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
            args.ProfilePictureId,
            args.Locale,
            args.TimeZone,
            role,
            app);

        return await CreateAccountAsync(
            account,
            args.Password)
            .ConfigureAwait(false);
    }

    private async Task<CreateAccountResult> CreateAccountAsync(
        Account account,
        string password)
    {
        var result = await CreateIdentityAndSaveAsync(
            account,
            password)
            .ConfigureAwait(false);

        try
        {
            await _unitOfWork
                .CommitChangesAsync()
                .ConfigureAwait(false);
        }
        catch (Exception)
        {
            await _identityRepository
                .DeleteByIdAsync(account.Id)
                .ConfigureAwait(false);

            throw;
        }

        return result;
    }

    private async Task AssertExistenseOfEmailAsync(string email)
    {
        var existAuth = await _accountRepository
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

        var roles = await GetRoleAsync(
            args.RoleIds,
            appIds,
            accountLogged.Tenant.Id)
            .ConfigureAwait(false);

        var account = Account.New(
            args.Email,
            args.FirstName,
            args.LastName,
            args.ProfilePictureId,
            args.Locale,
            args.TimeZone,
            roles,
            apps,
            accountLogged.Tenant);

        var identity = Identity.NewForAccount(account, args.Password);

        await _identityRepository
            .CreateAndSaveAsync(identity)
            .ConfigureAwait(false);

        await _accountRepository
            .CreateAsync(account)
            .ConfigureAwait(false);

        try
        {
            await _unitOfWork
                .CommitChangesAsync()
                .ConfigureAwait(false);
        }
        catch (Exception)
        {
            await _identityRepository
                .DeleteByIdAsync(account.Id)
                .ConfigureAwait(false);

            throw;
        }

        return account;
    }

    private async Task<List<Role>> GetRoleAsync(
        List<Guid> roleIds,
        List<Guid> appIds,
        Guid tenantId)
    {
        return await _roleRepository
            .GetByIdAsync(roleIds, appIds, tenantId)
            .ConfigureAwait(false);
    }
    #endregion

    public async Task UpdatePasswordAsync(
        UpdatePasswordArgs args,
        AccountLogged accountLogged)
    {
        await _identityRepository
            .UpdatePasswordByIdAsync(
            accountLogged.Id,
            args.NewPassword)
            .ConfigureAwait(false);
    }

    public async Task AssertByEmailAsync(string email)
    {
        var existAccount = await _accountRepository
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
        var accounts = await _accountRepository
            .GetAllAsync(accountLogged.Tenant.Id, page, pageSize)
            .ConfigureAwait(false);

        return accounts;
    }

    public async Task UpdateRolesAsync(
        Guid id,
        UpdateRolesArgs args,
        AccountLogged accountLogged)
    {
        var account = await _accountRepository
            .GetByIdAsync(id, accountLogged)
            .ConfigureAwait(false);

        var rolesToDelete = account
            .Roles
            .Where(r => !args.RolesIds.Contains(r.Id))
            .ToList();
        if (rolesToDelete.Count != 0)
        {
            var rolesIdsToDelete = rolesToDelete
                .Select(r => r.Id)
                .ToList();

            await _accountRepository
                .DeleteRolesByIdAsync(rolesIdsToDelete, account)
                .ConfigureAwait(false);
        }

        var newRoles = args
            .RolesIds
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

            await _accountRepository
                .AddRolesByIdAsync(newRoles, account)
                .ConfigureAwait(false);
        }

        await _unitOfWork
            .CommitChangesAsync()
            .ConfigureAwait(false);
    }
}
