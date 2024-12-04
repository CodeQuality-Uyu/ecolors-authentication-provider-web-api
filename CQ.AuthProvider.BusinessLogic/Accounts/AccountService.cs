using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.Utility;

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
            session.Token,
            account.Roles.ConvertAll(r => r.Name),
            account.Roles.SelectMany(r => r.Permissions.ConvertAll(p => p.Key)).ToList());

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
        firstName = Guard.Normalize(firstName);
        lastName = Guard.Normalize(lastName);
        var account = new Account
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            FullName = $"{firstName} {lastName}",
            ProfilePictureId = profilePictureId,
            Locale = locale,
            TimeZone = timeZone,
            Roles = [role],
            Tenant = app.Tenant,
            Apps = [app]
        };

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
            app.Tenant.Id)
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
        if(roleId == null)
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

    public async Task UpdatePasswordByCredentialsAsync(UpdatePasswordArgs args)
    {
        var identity = await _identityRepository
            .GetByCredentialsAsync(
            args.Email,
            args.Code)
            .ConfigureAwait(false);

        await _identityRepository
            .UpdatePasswordByIdAsync(
            identity.Id,
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

    public async Task<Account> GetByEmailAsync(string email)
    {
        return await _accountRepository
            .GetByEmailAsync(email)
            .ConfigureAwait(false);
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
}
