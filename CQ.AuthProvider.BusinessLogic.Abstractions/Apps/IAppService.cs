
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Apps;

public interface IAppService
{
}

internal interface IAppInternalService
    : IAppService
{
    Task<App> GetByIdAsync(
        string id,
        AccountLogged accountLogged);

    Task<List<App>> GetAllByIdAsync(
        List<string> ids,
        AccountLogged accountLogged);
}
