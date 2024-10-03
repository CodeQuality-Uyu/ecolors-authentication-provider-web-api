using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.Exceptions;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts;

internal sealed class AccountService(
    IAccountRepository _accountRepository,
    IIdentityRepository _identityRepository,
    ISessionInternalService _sessionService,
    IRoleInternalService _roleInternalService,
    IAppInternalService _appService,
    IUnitOfWork _unitOfWork)
    : IAccountInternalService
{
    #region Create
    public async Task<CreateAccountResult> CreateAsync(
        Account account,
        string password)
    {
        var identity = new Identity(
            account.Email,
            password)
        {
            Id = account.Id
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
        await AssertEmailInUseAsync(args.Email).ConfigureAwait(false);

        var role = await GetRoleAsync(args.RoleId).ConfigureAwait(false);
        var app = await _appService
            .GetByIdAsync(args.AppId)
            .ConfigureAwait(false);

        var account = new Account(
            args.Email,
            args.FirstName,
            args.LastName,
            args.ProfilePictureId,
            args.Locale,
            args.TimeZone,
            role,
            app);

        var result = await CreateAsync(
            account,
            args.Password)
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

    private async Task AssertEmailInUseAsync(string email)
    {
        var existAuth = await _accountRepository
            .ExistByEmailAsync(email)
            .ConfigureAwait(false);

        if (existAuth)
        {
            throw new SpecificResourceDuplicatedException<Account>(nameof(Account.Email), email);
        }
    }

    private async Task<Role> GetRoleAsync(string? roleId)
    {
        if (Guard.IsNull(roleId))
        {
            return await _roleInternalService
                .GetDefaultAsync()
                .ConfigureAwait(false);
        }

        return await _roleInternalService
            .GetByIdAsync(roleId)
            .ConfigureAwait(false);
    }
    #endregion

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
}
