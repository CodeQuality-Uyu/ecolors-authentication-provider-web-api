using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

public interface ISessionService
{
    Task<SessionCreated> CreateAsync(CreateSessionCredentialsArgs credentials);

    Task DeleteAsync(Account accountLogged);

    Task<bool> IsTokenValidAsync(string token);
}

public interface ISessionInternalService : ISessionService
{
    Task<Session> CreateAsync(Identity identity);

    Task<Session> GetByTokenAsync(string token);
}
