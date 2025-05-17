using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Apps;

public interface IAppRepository
{
    Task<App> GetByIdAsync(Guid id);

    Task<List<App>> GetByIdAsync(List<Guid> ids);

    Task<App> GetOrDefaultByDefaultAsync(Guid tenantId);

    Task RemoveDefaultByIdAsync(Guid id);

    Task<bool> ExistsByNameInTenantAsync(
        string name,
        Guid tenantId);

    Task CreateAsync(App app);

    Task<Pagination<App>> GetPaginationAsync(
        Guid tenantId,
        Guid? fatherAppId,
        int page,
        int pageSize);

    Task UpdateAndSaveColorsByIdAsync(
        Guid id,
        CreateAppCoverBackgroundColorArgs updates);

    Task<List<App>> GetByEmailAccountAsync(string email);
}
