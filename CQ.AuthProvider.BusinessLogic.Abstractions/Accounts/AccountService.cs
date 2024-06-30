using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.Exceptions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

internal sealed class AccountService(
        IAccountRepository accountRepository,
        IIdentityRepository identityService,
        ISessionInternalService sessionService,
        IRoleInternalService roleInternalService,
        IAppInternalService appInternalService)
    : IAccountInternalService
{
    #region Create
    public async Task<CreateAccountResult> CreateAsync(CreateAccountArgs args)
    {
        await AssertEmailInUseAsync(args.Email).ConfigureAwait(false);

        var identity = await CreateIdentityAsync(args).ConfigureAwait(false);

        try
        {
            var role = await GetRoleAsync(args.Role).ConfigureAwait(false);
            var app = await appInternalService
                .GetByIdAsync(args.AppId)
                .ConfigureAwait(false);

            var account = new Account(
                args.Email,
                args.FirstName,
                args.LastName,
                args.ProfilePictureUrl,
                args.Locale,
                args.TimeZone,
                role,
                app,
                app.Tenant)
            {
                Id = identity.Id
            };

            await accountRepository
                .CreateAsync(account)
                .ConfigureAwait(false);

            var session = await sessionService
                .CreateAsync(identity)
                .ConfigureAwait(false);

            var accountToReturn = new CreateAccountResult(
                account.Id,
                account.Email,
                account.FullName,
                account.FirstName,
                account.LastName,
                account.ProfilePictureUrl,
                account.Locale,
                account.TimeZone,
                session.Token,
                account.Roles.ConvertAll(r => r.Key));

            return accountToReturn;
        }
        catch (SpecificResourceNotFoundException<Role>)
        {
            await identityService
                .DeleteByIdAsync(identity.Id)
                .ConfigureAwait(false);
            throw;
        }
    }

    private async Task AssertEmailInUseAsync(string email)
    {
        var existAuth = await accountRepository
            .ExistByEmailAsync(email)
            .ConfigureAwait(false);

        if (existAuth)
        {
            throw new SpecificResourceDuplicatedException<Account>(nameof(Account.Email), email);
        }
    }

    private async Task<Identity> CreateIdentityAsync(CreateAccountArgs newAccount)
    {
        var identity = new Identity(
            newAccount.Email,
            newAccount.Password);

        await identityService
            .CreateAsync(identity)
            .ConfigureAwait(false);

        return identity;
    }

    private async Task<Role> GetRoleAsync(RoleKey? roleKey)
    {
        if (Guard.IsNull(roleKey))
        {
            return await roleInternalService
                .GetDefaultAsync()
                .ConfigureAwait(false);
        }

        return await roleInternalService
            .GetByKeyAsync(roleKey)
            .ConfigureAwait(false);
    }
    #endregion

    public async Task UpdatePasswordAsync(
        string newPassword,
        Account userLogged)
    {
        Guard.ThrowIsInputInvalidPassword(newPassword, nameof(newPassword));

        await identityService
            .UpdatePasswordAsync(
            userLogged.Id,
            newPassword)
            .ConfigureAwait(false);
    }

    public async Task<AccountLogged> GetByTokenAsync(string token)
    {
        var session = await sessionService
            .GetByTokenAsync(token)
            .ConfigureAwait(false);

        var account = await accountRepository
            .GetByIdAsync(session.Account.Id)
            .ConfigureAwait(false);

        return new AccountLogged(account, token);
    }

    public async Task<Account> GetByEmailAsync(string email)
    {
        return await accountRepository
            .GetByEmailAsync(email)
            .ConfigureAwait(false);
    }

    public async Task<Account> GetByIdAsync(string id)
    {
        return await accountRepository
            .GetByIdAsync(id)
            .ConfigureAwait(false);
    }
}
