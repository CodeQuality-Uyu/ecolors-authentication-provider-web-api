namespace CQ.AuthProvider.BusinessLogic.Sessions
{
    public interface ISessionService
    {
        Task<SessionCreated> CreateAsync(CreateSessionCredentials credentials);

        Task<Session> GetByTokenAsync(string token);

        Task<Session?> GetOrDefaultByTokenAsync(string token);

        Task<bool> IsTokenValidAsync(string token);
    }
}
