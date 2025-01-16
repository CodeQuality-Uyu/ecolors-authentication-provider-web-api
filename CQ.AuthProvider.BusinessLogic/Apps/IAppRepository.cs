using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Apps;

public interface IAppRepository
{
    Task<App> GetByIdAsync(Guid id);

    Task<App> GetOrDefaultByDefaultAsync(Guid tenantId);

    Task RemoveDefaultByIdAsync(Guid id);

    Task<List<App>> GetByIdsAsync(List<Guid> ids);

    Task<bool> ExistsByNameInTenantAsync(
        string name,
        Guid tenantId);

    Task CreateAndSaveAsync(App app);

    Task<Pagination<App>> GetAllAsync(
        Guid tenantId,
        int page,
        int pageSize);
}
