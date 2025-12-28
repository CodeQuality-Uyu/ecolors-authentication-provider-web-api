using CQ.ApiElements;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Sessions;

public sealed class SessionService(
    ISessionRepository sessionRepository,
    IIdentityRepository identityRepository,
    IAccountRepository accountRepository,
    ITokenService tokenService,
    IUnitOfWork _unitOfWork)
    : ISessionInternalService
{
    public async Task<Session> CreateAsync(CreateSessionCredentialsArgs args)
    {
        var identity = await identityRepository
            .GetByCredentialsAsync(args.Email, args.Password)
            .ConfigureAwait(false);

        var account = await accountRepository
            .GetByIdAsync(identity.Id, args.AppId)
            .ConfigureAwait(true);

        var app = account
            .Apps
            .FirstOrDefault(a => a.Id == args.AppId);

        if (Guard.IsNull(app))
        {
            throw new InvalidOperationException($"Account ({account.Email}) doesn't exist in app ({args.AppId})");
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
        var token = await tokenService
            .CreateAsync(account)
            .ConfigureAwait(false);

        var session = new Session(
            account,
            app,
            token);

        await sessionRepository
            .CreateAsync(session)
            .ConfigureAwait(false);

        return session;
    }

    public async Task DeleteAsync(AccountLogged accountLogged)
    {
        await sessionRepository
            .DeleteByTokenAsync(accountLogged.Token)
            .ConfigureAwait(false);
    }

    public async Task<object> GetByHeaderAsync(string value)
    {
        var session = await sessionRepository
            .GetByTokenAsync(value)
            .ConfigureAwait(false);

        var account = new AccountLogged(
            session.Account,
            value,
            session.App);

        return account;
    }
}
