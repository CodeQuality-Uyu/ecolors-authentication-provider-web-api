using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;

namespace CQ.AuthProvider.BusinessLogic.Sessions
{
    public interface ISessionService
    {
        Task<SessionCreated> CreateAsync(CreateSessionCredentialsArgs credentials);

        Task DeleteAsync(Account accountLogged);

        Task<Session> GetByTokenAsync(string token);

        Task<Session?> GetOrDefaultByTokenAsync(string token);

        Task<bool> IsTokenValidAsync(string token);
    }

    public interface ISessionInternalService : ISessionService
    {
        Task<Session> CreateAsync(Identity identity);
    }
}
