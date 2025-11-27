using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Apps;

internal sealed class AppService(
    IAppRepository appRepository,
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork,
    IBlobService blobService)
    : IAppInternalService
{
    public async Task<App> CreateAsync(
        CreateAppArgs args,
        AccountLogged accountLogged)
    {
        await AssertAsync(appRepository, args, accountLogged).ConfigureAwait(false);

        var app = new App(
            args.Name,
            args.IsDefault,
            args.CoverId,
            args.BackgroundColors != null ? new()
            {
                Colors = args.BackgroundColors.Colors,
                Config = args.BackgroundColors.Config
            } : null,
            args.BackgroundCoverId,
            accountLogged.Tenant,
            null);

        if (app.IsDefault)
        {
            var defaultApp = await appRepository
                .GetOrDefaultByDefaultAsync(app.Tenant.Id)
                .ConfigureAwait(false);
            if (Guard.IsNotNull(defaultApp))
            {
                await appRepository
                    .RemoveDefaultByIdAsync(defaultApp.Id)
                    .ConfigureAwait(false);
            }
        }

        await appRepository
            .CreateAsync(app)
            .ConfigureAwait(false);

        if (args.RegisterToIt)
        {
            await accountRepository
               .AddAppAsync(app, accountLogged)
               .ConfigureAwait(false);
        }

        await blobService
            .MoveAppElementAsync(
            accountLogged.AppLogged,
            app,
            app.CoverId)
            .ConfigureAwait(false);

        if (app.BackgroundCoverId.HasValue)
        {
            await blobService
            .MoveAppElementAsync(
                accountLogged.AppLogged,
                app,
                app.BackgroundCoverId.Value)
            .ConfigureAwait(false);
        }

        await unitOfWork
            .CommitChangesAsync()
            .ConfigureAwait(false);

        return app;

        static async Task AssertAsync(IAppRepository appRepository, CreateAppArgs args, AccountLogged accountLogged)
        {
            var existAppWithName = await appRepository
                        .ExistsByNameInTenantAsync(args.Name, accountLogged.Tenant.Id);
            if (existAppWithName)
            {
                throw new InvalidOperationException("Name is in used");
            }
        }
    }

    public async Task<App> CreateClientAsync(
        CreateClientAppArgs args,
        AccountLogged accountLogged)
    {
        var existAppWithName = await appRepository
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
            false,
            args.CoverId ?? accountLogged.AppLogged.CoverId,
            backgroundColors ?? accountLogged.AppLogged.BackgroundColor,
            args.BackgroundCoverId ?? accountLogged.AppLogged.BackgroundCoverId,
            accountLogged.Tenant,
            accountLogged.AppLogged);

        await appRepository
            .CreateAsync(app)
            .ConfigureAwait(false);

        if (args.CoverId.HasValue)
        {
            await blobService
                .MoveAppElementAsync(
                accountLogged.AppLogged,
                app,
                app.CoverId)
                .ConfigureAwait(false);
        }

        if (app.BackgroundCoverId.HasValue)
        {
            await blobService
            .MoveAppElementAsync(
                accountLogged.AppLogged,
                app,
                app.BackgroundCoverId.Value)
            .ConfigureAwait(false);
        }

        await unitOfWork
            .CommitChangesAsync()
            .ConfigureAwait(false);

        return app;
    }

    public async Task<App> GetByIdAsync(Guid id)
    {
        var app = await appRepository
            .GetByIdAsync(id)
            .ConfigureAwait(false);

        return app;
    }

    public async Task<List<App>> GetByIdAsync(List<Guid> ids)
    {
        var apps = await appRepository
            .GetByIdAsync(ids)
            .ConfigureAwait(false);

        return apps;
    }

    public async Task<Pagination<App>> GetPaginationAsync(
        Guid? fatherAppId,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var hasListAppsPermission = accountLogged.HasPermission("getall-app");
        var hasListOwnClientsPermission = accountLogged.HasPermission("getall-client");

        if (!hasListAppsPermission && hasListOwnClientsPermission)
        {
            fatherAppId = accountLogged.AppLogged.Id;
        }

        var apps = await appRepository
            .GetPaginationAsync(
            accountLogged.Tenant.Id,
            fatherAppId,
            page,
            pageSize)
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

        await appRepository
            .UpdateAndSaveColorsByIdAsync(id, args)
            .ConfigureAwait(false);
    }

    public async Task<List<App>> GetByEmailAccountAsync(string email)
    {
        var apps = await appRepository
            .GetByEmailAccountAsync(email)
            .ConfigureAwait(false);

        return apps;
    }
}
