using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

public interface ISessionService
{
    Task<Session> CreateAsync(CreateSessionCredentialsArgs args);

    Task DeleteAsync(AccountLogged accountLogged);

    Task<bool> IsTokenValidAsync(string token);
}

public interface ISessionInternalService : ISessionService
{
    Task<Session> CreateAsync(
        Identity identity,
        string appId);

    Task<Session> GetByTokenAsync(string token);
}
