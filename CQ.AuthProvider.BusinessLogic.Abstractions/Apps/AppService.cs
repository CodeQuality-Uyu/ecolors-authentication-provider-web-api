

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Apps;

internal sealed class AppService
    : IAppInternalService
{
    public Task<List<App>> GetAllByIdAsync(List<string> ids)
    {
        throw new NotImplementedException();
    }

    public Task<App> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}
