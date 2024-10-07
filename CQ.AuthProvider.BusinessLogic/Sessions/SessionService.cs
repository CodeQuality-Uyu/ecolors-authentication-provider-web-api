using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
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

        var app = account
            .Apps
            .FirstOrDefault(a => (args.AppId == null && a.IsDefault) || a.Id == args.AppId);

        if (Guard.IsNull(app))
        {
            throw new InvalidOperationException($"Account of email {account.Email} doesn't exist {(Guard.IsNullOrEmpty(args.AppId) ? $"in defualt app of tenant {account.TenantValue.Name}" : $"in app {args.AppId}")}");
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

    public async Task DeleteAsync(AccountLogged accountLogged)
    {
        await _sessionRepository
            .DeleteByTokenAsync(accountLogged.Token)
            .ConfigureAwait(false);
    }

    public async Task<AccountLogged> GetAccountByTokenAsync(string token)
    {
        var session = await _sessionRepository
            .GetByTokenAsync(token)
            .ConfigureAwait(false);

        return new AccountLogged(
            session.Account,
            token,
            session.App);
    }
}
