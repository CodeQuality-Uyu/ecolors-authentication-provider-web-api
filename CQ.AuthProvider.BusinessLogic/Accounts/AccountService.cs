using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.Abstractions.Repositories;

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

        return await CreateAccountAsync(
            args.Email,
            args.Password,
            args.FirstName,
            args.LastName,
            args.ProfilePictureId,
            args.Locale,
            args.TimeZone,
            app,
            role)
            .ConfigureAwait(false);
    }

    private async Task<CreateAccountResult> CreateAccountAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        string? profilePictureId,
        string locale,
        string timeZone,
        App app,
        Role role)
    {
        var account = Account.New(
            email,
            firstName,
            lastName,
            profilePictureId,
            locale,
            timeZone,
            role,
            app);

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

    public async Task<CreateAccountResult> CreateAndSaveAsync(
        CreateAccountForArgs args,
        AccountLogged accountLogged)
    {
        await AssertExistenseOfEmailAsync(args.Email).ConfigureAwait(false);

        var app = await _appService
            .GetByIdAsync(args.AppId ?? accountLogged.AppLogged.Id)
            .ConfigureAwait(false);

        var role = await GetRoleAsync(
            args.RoleId,
            app.Id,
            accountLogged.Tenant.Id)
            .ConfigureAwait(false);

        return await CreateAccountAsync(
            args.Email,
            args.Password,
            args.FirstName,
            args.LastName,
            args.ProfilePictureId,
            args.Locale,
            args.TimeZone,
            app,
            role)
            .ConfigureAwait(false);
    }

    private async Task<Role> GetRoleAsync(
        Guid? roleId,
        Guid appId,
        Guid tenantId)
    {
        if (roleId == null)
        {
            return await _roleRepository
                .GetDefaultByTenantIdAsync(appId, tenantId)
                .ConfigureAwait(false);
        }

        return await _roleRepository
            .GetByIdAsync(roleId.Value)
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

            if(roles.Count != newRoles.Count)
            {
                throw new InvalidOperationException("Some roles don't belong to tenant");
            }

            var rolesNotInApps = roles
                .Where(r => !account.Apps.Exists(a => a.Id == r.AppId))
                .ToList();
            if(rolesNotInApps.Count != 0)
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
