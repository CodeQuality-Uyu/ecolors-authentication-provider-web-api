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
    public async Task CreateAndSaveAsync(App app)
    {
        var appEfCore = new AppEfCore(app);

        await CreateAndSaveAsync(appEfCore).ConfigureAwait(false);
    }

    public async Task<bool> ExistsByNameInTenantAsync(string name, Guid tenantId)
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

    public async Task<List<App>> GetByIdsAsync(List<Guid> ids)
    {
        var apps = await GetAllAsync(a => ids.Contains(a.Id))
            .ConfigureAwait(false);

        return _mapper.Map<List<App>>(apps);
    }

    async Task<App> IAppRepository.GetByIdAsync(Guid id)
    {
        var app = await GetByIdAsync(id)
            .ConfigureAwait(false);

        return _mapper.Map<App>(app);
    }

    public async Task<Pagination<App>> GetAllAsync(
        Guid tenantId,
        int page,
        int pageSize)
    {
        var query = ConcreteContext
            .Apps
            .Where(a => a.TenantId == tenantId)
            .Paginate(page, pageSize);

        var pagiantion = await query
            .ToPaginateAsync(page, pageSize)
            .ConfigureAwait(false);

        return _mapper.Map<Pagination<App>>(pagiantion);
    }
}
