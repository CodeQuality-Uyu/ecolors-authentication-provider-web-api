using CQ.AuthProvider.Abstractions;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Sessions;

public sealed class SessionService(
    ISessionRepository _sessionRepository,
    IIdentityRepository _identityRepository,
    IAccountRepository _accountRepository,
    ITokenService _tokenService,
    IUnitOfWork _unitOfWork)
    : ISessionInternalService,
    IBearerLoggedService
{
    public async Task<Session> CreateAndSaveAsync(CreateSessionCredentialsArgs args)
    {
        var identity = await _identityRepository
            .GetByCredentialsAsync(args.Email, args.Password)
            .ConfigureAwait(false);

        var account = await _accountRepository
            .GetByIdAsync(identity.Id)
            .ConfigureAwait(true);

        var app = account
            .Apps
            .FirstOrDefault(a => (args.AppId == null && a.IsDefault) || a.Id == args.AppId);

        if (Guard.IsNull(app))
        {
            throw new InvalidOperationException($"Account of email {account.Email} doesn't exist {(args.AppId == null ? $"in defualt app of tenant {account.Tenant.Name}" : $"in app {args.AppId}")}");
        }

        var session = await CreateAsync(account, app).ConfigureAwait(false);

        await _unitOfWork
            .CommitChangesAsync()
            .ConfigureAwait(false);

        return session;
    }

    public async Task<Session> CreateAsync(
        Account account,
        App app)
    {
        var token = await _tokenService
            .CreateAsync(account)
            .ConfigureAwait(false);

        var session = new Session(
            account,
            app,
            token);

        await _sessionRepository
            .CreateAsync(session)
            .ConfigureAwait(false);

        return session;
    }

    public async Task DeleteAsync(AccountLogged accountLogged)
    {
        await _sessionRepository
            .DeleteByTokenAsync(accountLogged.Token)
            .ConfigureAwait(false);
    }

    public async Task<object> GetByHeaderAsync(string value)
    {
        var session = await _sessionRepository
            .GetByTokenAsync(value)
            .ConfigureAwait(false);

        var account = new AccountLogged(
            session.Account,
            value,
            session.App);

        return account;
    }
}
