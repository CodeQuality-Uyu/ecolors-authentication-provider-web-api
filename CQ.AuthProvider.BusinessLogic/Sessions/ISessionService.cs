using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;

namespace CQ.AuthProvider.BusinessLogic.Sessions;

public interface ISessionService
{
    Task<Session> CreateAsync(CreateSessionCredentialsArgs args);

    Task DeleteAsync(AccountLogged accountLogged);
}

public interface ISessionInternalService
    : ISessionService
{
    Task<Session> CreateAsync(
        Account account,
        App app);
}
