
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Apps;

public interface IAppService
{
}

internal interface IAppInternalService
    : IAppService
{
    Task<App> GetByIdAsync(string id);

    Task<App> GetByIdAsync(
        string id,
        AccountLogged accountLogged);

    Task<App> GetByIdAsync(
        string id,
        Tenant tenant);

    Task<List<App>> GetAllByIdAsync(
        List<string> ids,
        AccountLogged accountLogged);
}
