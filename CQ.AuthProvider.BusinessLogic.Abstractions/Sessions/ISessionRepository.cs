
namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

internal interface ISessionRepository
{
    Task CreateAsync(Session session);

    Task<Session> GetByTokenAsync(string token);

    Task DeleteByTokenAsync(string token);
}
