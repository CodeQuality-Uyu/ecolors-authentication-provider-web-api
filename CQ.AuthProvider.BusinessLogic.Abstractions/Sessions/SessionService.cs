using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions.Exceptions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

internal sealed class SessionService(
    IRepository<Session> sessionRepository,
    IIdentityRepository identityRepository,
    IAccountRepository accountRepository)
    : ISessionInternalService
{
    public async Task<SessionCreated> CreateAsync(CreateSessionCredentialsArgs credentials)
    {
        var identity = await identityRepository
            .GetByCredentialsAsync(credentials.Email, credentials.Password)
            .ConfigureAwait(false);

        if (Guard.IsNull(identity))
        {
            throw new InvalidCredentialsException(credentials.Email);
        }

        var session = await CreateAsync(identity).ConfigureAwait(false);

        var account = await accountRepository
            .GetByIdAsync(identity.Id)
            .ConfigureAwait(true);

        var sessionSaved = new SessionCreated(
            account,
            session.Token);

        return sessionSaved;
    }

    public async Task<Session> CreateAsync(Identity identity)
    {
        var session = new Session(identity.Id);

        await sessionRepository
            .CreateAsync(session)
            .ConfigureAwait(false);

        return session;
    }

    public async Task<Session> GetByTokenAsync(string token)
    {
        Db.ThrowIsInvalidId(token, nameof(token));

        var result = await sessionRepository
            .GetOrDefaultAsync(s => s.Token == token)
            .ConfigureAwait(false);

        if (result == null)
        {
            throw new ArgumentException("Invalid token, session has expired", nameof(token));
        }

        return result;
    }

    public Task<bool> IsTokenValidAsync(string token)
    {
        var isGuid = Guid.TryParse(token, out var id);

        return Task.FromResult(isGuid);
    }

    public async Task DeleteAsync(Account accountLogged)
    {
        await sessionRepository
            .DeleteAsync(s => s.Token == accountLogged.Token)
            .ConfigureAwait(false);
    }
}
