using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Apps;

internal sealed class AppService(
    IAppRepository _appRepository)
    : IAppInternalService
{
    public async Task CreateAsync(
        CreateAppArgs args,
        AccountLogged accountLogged)
    {
        var existAppWithName = await _appRepository
            .ExistsByNameInTenantAsync(args.Name, accountLogged.TenantValue.Id);
        if (existAppWithName)
        {
            throw new InvalidOperationException("Name is in used");
        }

        var app = new App(
            args.Name,
            args.IsDefault,
            accountLogged.TenantValue);

        await _appRepository
            .CreateAndSaveAsync(app)
            .ConfigureAwait(false);
    }

    public async Task<App> GetByIdAsync(string id)
    {
        var app = await _appRepository
            .GetByIdAsync(id)
            .ConfigureAwait(false);

        return app;
    }

    public async Task<Pagination<App>> GetAllAsync(
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var apps = await _appRepository
            .GetAllAsync(accountLogged.TenantValue.Id, page, pageSize)
            .ConfigureAwait(false);

        return apps;
    }
}
