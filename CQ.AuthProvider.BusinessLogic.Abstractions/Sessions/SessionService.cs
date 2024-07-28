using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions.Exceptions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tokens;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

internal sealed class SessionService(
    ISessionRepository sessionRepository,
    IIdentityRepository identityRepository,
    IAccountRepository accountRepository,
    ITokenService tokenService,
    IAppInternalService appService)
    : ISessionInternalService
{
    public async Task<Session> CreateAsync(CreateSessionCredentialsArgs args)
    {
        var identity = await identityRepository
            .GetByCredentialsAsync(args.Email, args.Password)
            .ConfigureAwait(false);

        if (Guard.IsNull(identity))
        {
            throw new InvalidCredentialsException(args.Email);
        }

        var session = await CreateAsync(
            identity,
            args.AppId)
            .ConfigureAwait(false);

        return session;
    }

    public async Task<Session> CreateAsync(
        Identity identity,
        string? appId)
    {
        var account = await accountRepository
            .GetByIdAsync(identity.Id)
            .ConfigureAwait(true);

        var app = account
            .Apps
            .FirstOrDefault(a => (appId == null && a.IsDefault) || a.Id == appId);

        if (Guard.IsNull(app))
        {
            throw new InvalidOperationException($"Account of email {identity.Email} doesn't exist {(Guard.IsNullOrEmpty(appId) ? $"in defualt app of tenant {account.Tenant.Name}" : $"in app {appId}")}");
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
            .GetOrDefaultByTokenAsync(token)
            .ConfigureAwait(false);

        if (Guard.IsNull(session))
        {
            throw new ArgumentException("Invalid token, session has expired", nameof(token));
        }

        return session;
    }

    public Task<bool> IsTokenValidAsync(string token)
    {
        var isGuid = Guid.TryParse(token, out var id);

        return Task.FromResult(isGuid);
    }

    public async Task DeleteAsync(AccountLogged accountLogged)
    {
        await sessionRepository
            .DeleteByTokenAsync(accountLogged.Token)
            .ConfigureAwait(false);
    }
}
