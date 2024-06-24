using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions.Exceptions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

internal sealed class SessionService(
    ISessionRepository sessionRepository,
    IIdentityRepository identityRepository,
    IAccountRepository accountRepository)
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


        var session = await CreateAsync(identity).ConfigureAwait(false);

        return session;
    }

    public async Task<Session> CreateAsync(Identity identity)
    {
        var account = await accountRepository
            .GetByIdAsync(identity.Id)
            .ConfigureAwait(true);

        var session = new Session(
            account,
            Db.NewId());

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
