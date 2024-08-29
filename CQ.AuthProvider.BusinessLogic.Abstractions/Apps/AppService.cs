namespace CQ.AuthProvider.BusinessLogic.Abstractions.Apps;

internal sealed class AppService(
    IAppRepository appRepository)
    : IAppInternalService
{
    public Task<App> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}
