using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.UnitOfWork.EfCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.EfCore.Apps;

internal sealed class AppRepository(
    AuthDbContext context,
    [FromKeyedServices(MapperKeyedService.DataAccess)] IMapper _mapper)
    : AuthDbContextRepository<AppEfCore>(context),
    IAppRepository
{
    public async Task CreateAsync(App app)
    {
        var appEfCore = _mapper.Map<AppEfCore>(app);

        await CreateAsync(appEfCore).ConfigureAwait(false);
    }

    public async Task<bool> ExistsByNameInTenantAsync(
        string name,
        Guid tenantId)
    {
        var query = ConcreteContext
            .Apps
            .Where(a => EF.Functions.Like(a.Name, name))
            .Where(a => a.TenantId == tenantId);

        var exist = await query
            .AnyAsync()
            .ConfigureAwait(false);

        return exist;
    }

    public async Task<List<App>> GetByIdAsync(List<Guid> ids)
    {
        var apps = await GetAllAsync(a => ids.Contains(a.Id))
            .ConfigureAwait(false);

        return _mapper.Map<List<App>>(apps);
    }

    async Task<App> IAppRepository.GetByIdAsync(Guid id)
    {
        var query = Entities
            .Include(a => a.Tenant)
            .Where(a => a.Id == id);

        var app = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        AssertNullEntity(app, id, nameof(App.Id));

        return _mapper.Map<App>(app);
    }

    public async Task<Pagination<App>> GetAllAsync(
        Guid tenantId,
        int page,
        int pageSize)
    {
        var pagination = await Entities
            .ToPaginateAsync(
            a => a.TenantId == tenantId,
            page,
            pageSize)
            .ConfigureAwait(false);

        return _mapper.Map<Pagination<App>>(pagination);
    }

    public async Task<App> GetOrDefaultByDefaultAsync(Guid tenantId)
    {
        var query = Entities
            .Where(a => a.IsDefault)
            .Where(a => a.TenantId == tenantId);

        var app = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        return _mapper.Map<App>(app);
    }

    public async Task RemoveDefaultByIdAsync(Guid id)
    {
        var app = await GetByIdAsync(id).ConfigureAwait(false);

        app.IsDefault = false;
    }

    public async Task UpdateAndSaveColorsByIdAsync(
        Guid id,
        CreateAppCoverBackgroundColorArgs updates)
    {
        var backgroundColors = new CoverBackgroundColorEfCore
        {
            Colors = updates.Colors,
            Config = updates.Config
        };

        await Entities
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(setter => setter.SetProperty(a => a.BackgroundColor, backgroundColors))
            .ConfigureAwait(false)
            ;
    }
}
