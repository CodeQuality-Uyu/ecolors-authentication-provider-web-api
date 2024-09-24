using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Tokens;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Sessions;

internal sealed class SessionService(
    ISessionRepository sessionRepository,
    IIdentityRepository identityRepository,
    IAccountRepository accountRepository,
    ITokenService tokenService)
    : ISessionInternalService
{
    public async Task<Session> CreateAsync(CreateSessionCredentialsArgs args)
    {
        var identity = await identityRepository
            .GetByCredentialsAsync(args.Email, args.Password)
            .ConfigureAwait(false);

        var account = await accountRepository
            .GetByIdAsync(identity.Id)
            .ConfigureAwait(true);

        var session = await CreateAsync(
            account,
            args.AppId)
            .ConfigureAwait(false);

        return session;
    }

    public async Task<Session> CreateAsync(
        Account account,
        string? appId)
    {
        var app = account
            .Apps
            .FirstOrDefault(a => appId == null && a.IsDefault || a.Id == appId);

        if (Guard.IsNull(app))
        {
            throw new InvalidOperationException($"Account of email {account.Email} doesn't exist {(Guard.IsNullOrEmpty(appId) ? $"in defualt app of tenant {account.Tenant.Name}" : $"in app {appId}")}");
        }

        var token = tokenService.Create();

        var session = new Session(
            account,
            app,
            token);

        await sessionRepository
            .CreateAsync(session)
            .ConfigureAwait(false);

        return session;
    }

    public async Task<Session> GetByTokenAsync(string token)
    {
        Db.ThrowIsInvalidId(token, nameof(token));

        var session = await sessionRepository
            .GetByTokenAsync(token)
            .ConfigureAwait(false);

        return session;
    }

    public async Task DeleteAsync(AccountLogged accountLogged)
    {
        await sessionRepository
            .DeleteByTokenAsync(accountLogged.Token)
            .ConfigureAwait(false);
    }
}
