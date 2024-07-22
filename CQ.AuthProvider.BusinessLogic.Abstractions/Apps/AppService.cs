using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Apps;

internal sealed class AppService
    : IAppInternalService
{
    public Task<List<App>> GetAllByIdAsync(
        List<string> ids,
        AccountLogged accountLogged)
    {
        throw new NotImplementedException();
    }

    public Task<App> GetByIdAsync(
        string id,
        AccountLogged accountLogged)
    {
        throw new NotImplementedException();
    }

    public Task<App> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<App> GetByIdAsync(string id, Tenant tenant)
    {
        throw new NotImplementedException();
    }
}
