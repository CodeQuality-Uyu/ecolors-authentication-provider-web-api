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
        IRoleInternalService roleInternalService)
    : IAccountInternalService
{
    #region Create
    public async Task<CreateAccountResult> CreateAsync(CreateAccountArgs newAccount)
    {
        await AssertEmailInUseAsync(newAccount.Email).ConfigureAwait(false);

        var identity = await CreateIdentityAsync(newAccount).ConfigureAwait(false);

        try
        {
            var role = await GetRoleAsync(newAccount.Role).ConfigureAwait(false);

            var account = new Account(
                newAccount.Email,
                newAccount.FirstName,
                newAccount.LastName,
                newAccount.FullName,
                newAccount.ProfilePictureUrl,
                newAccount.Locale,
                newAccount.TimeZone,
                [role])
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

    public async Task<Account> GetByTokenAsync(string token)
    {
        var session = await sessionService
            .GetByTokenAsync(token)
            .ConfigureAwait(false);

        var account = await accountRepository
            .GetByIdAsync(session.AccountId)
            .ConfigureAwait(false);

        return account;
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
