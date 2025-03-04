using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Apps;

internal sealed class AppService(
    IAppRepository _appRepository,
    IAccountRepository _accountRepository,
    IUnitOfWork _unitOfWork,
    IBlobService _blobService)
    : IAppInternalService
{
    public async Task CreateAsync(
        CreateAppArgs args,
        AccountLogged accountLogged)
    {
        var existAppWithName = await _appRepository
            .ExistsByNameInTenantAsync(args.Name, accountLogged.Tenant.Id);
        if (existAppWithName)
        {
            throw new InvalidOperationException("Name is in used");
        }

        CoverBackgroundColor? backgroundColors = null;
        if (args.BackgroundColors != null)
        {
            backgroundColors = new()
            {
                Colors = args.BackgroundColors.Colors,
                Config = args.BackgroundColors.Config
            };
        }

        var app = new App(
            args.Name,
            args.IsDefault,
            args.CoverId,
            backgroundColors,
            args.BackgroundCoverId,
            accountLogged.Tenant);

        if (app.IsDefault)
        {
            var defaultApp = await _appRepository
                .GetOrDefaultByDefaultAsync(app.Tenant.Id)
                .ConfigureAwait(false);
            if (Guard.IsNotNull(defaultApp))
            {
                await _appRepository
                    .RemoveDefaultByIdAsync(defaultApp.Id)
                    .ConfigureAwait(false);
            }
        }

        await _appRepository
            .CreateAsync(app)
            .ConfigureAwait(false);

        //await _accountRepository
        //    .AddAppAsync(app, accountLogged)
        //    .ConfigureAwait(false);

        await _blobService
            .MoveAppElementAsync(
            accountLogged.AppLogged,
            app,
            app.CoverId)
            .ConfigureAwait(false);

        if (args.BackgroundCoverId.HasValue)
        {
            await _blobService
            .MoveAppElementAsync(
                accountLogged.AppLogged,
                app,
                app.BackgroundCoverId.Value)
            .ConfigureAwait(false);
        }

        await _unitOfWork
            .CommitChangesAsync()
            .ConfigureAwait(false);
    }

    public async Task<App> GetByIdAsync(Guid id)
    {
        var app = await _appRepository
            .GetByIdAsync(id)
            .ConfigureAwait(false);

        return app;
    }

    public async Task<List<App>> GetByIdAsync(List<Guid> ids)
    {
        var apps = await _appRepository
            .GetByIdAsync(ids)
            .ConfigureAwait(false);

        return apps;
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

    public async Task UpdateColorsByIdAsync(
        Guid id,
        CreateAppCoverBackgroundColorArgs args,
        AccountLogged accountLogged)
    {
        var appIsNotOfAccount = !accountLogged.AppsIds.Contains(id);
        if (appIsNotOfAccount)
        {
            throw new InvalidOperationException("Account doesn't belong to app");
        }

        await _appRepository
            .UpdateAndSaveColorsByIdAsync(id, args)
            .ConfigureAwait(false);
    }

    public async Task<List<App>> GetByEmailAccountAsync(string email)
    {
        var apps = await _appRepository
            .GetByEmailAccountAsync(email)
            .ConfigureAwait(false);

        return apps;
    }
}
