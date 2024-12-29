using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Licenses;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Apps;

internal sealed class AppService(
    IAppRepository _appRepository,
    ILicenseRepository _licenseRepository,
    IUnitOfWork _unitOfWork)
    : IAppInternalService
{
    public async Task<AppCreatedResult> CreateAsync(
        CreateAppArgs args,
        AccountLogged accountLogged)
    {
        var existAppWithName = await _appRepository
            .ExistsByNameInTenantAsync(args.Name, accountLogged.Tenant.Id);
        if (existAppWithName)
        {
            throw new InvalidOperationException("Name is in used");
        }

        var app = new App(
            args.Name,
            args.IsDefault,
            accountLogged.Tenant);
        await _appRepository
            .CreateAsync(app)
            .ConfigureAwait(false);

        var license = new License(app);
        await _licenseRepository
            .CreateAsync(license)
            .ConfigureAwait(false);

        await _unitOfWork
            .CommitChangesAsync()
            .ConfigureAwait(false);

        return new AppCreatedResult(license.Token);
    }

    public async Task<App> GetByIdAsync(Guid id)
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
            .GetAllAsync(accountLogged.Tenant.Id, page, pageSize)
            .ConfigureAwait(false);

        return apps;
    }
}
