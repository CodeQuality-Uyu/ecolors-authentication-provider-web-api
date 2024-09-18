namespace CQ.AuthProvider.BusinessLogic.Apps;

internal sealed class AppService(
    IAppRepository appRepository)
    : IAppInternalService
{
    public async Task<App> GetByIdAsync(string id)
    {
        var app = await appRepository
            .GetByIdAsync(id)
            .ConfigureAwait(false);

        return app;
    }
}
