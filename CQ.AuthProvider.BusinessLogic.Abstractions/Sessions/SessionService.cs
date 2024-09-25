using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Tokens;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Sessions;

internal sealed class SessionService(
    ISessionRepository _sessionRepository,
    IIdentityRepository _identityRepository,
    IAccountRepository _accountRepository,
    ITokenService _tokenService,
    IUnitOfWork _unitOfWork)
    : ISessionInternalService
{
    public async Task<Session> CreateAndSaveAsync(CreateSessionCredentialsArgs args)
    {
        var identity = await _identityRepository
            .GetByCredentialsAsync(args.Email, args.Password)
            .ConfigureAwait(false);

        var account = await _accountRepository
            .GetByIdAsync(identity.Id)
            .ConfigureAwait(true);

        var session = await CreateAndSaveAsync(
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

        var token = _tokenService.Create();

        var session = new Session(
            account,
            app,
            token);

        await _sessionRepository
            .CreateAsync(session)
            .ConfigureAwait(false);

        return session;
    }

    public async Task<Session> CreateAndSaveAsync(
        Account account,
        string? appId)
    {
        var session = await CreateAsync(
            account,
            appId)
            .ConfigureAwait(false);

        await _unitOfWork
            .CommitChangesAsync()
            .ConfigureAwait(false);

        return session;
    }

    public async Task<Session> GetByTokenAsync(string token)
    {
        Db.ThrowIsInvalidId(token, nameof(token));

        var session = await _sessionRepository
            .GetByTokenAsync(token)
            .ConfigureAwait(false);

        return session;
    }

    public async Task DeleteAsync(AccountLogged accountLogged)
    {
        await _sessionRepository
            .DeleteByTokenAsync(accountLogged.Token)
            .ConfigureAwait(false);
    }
}
