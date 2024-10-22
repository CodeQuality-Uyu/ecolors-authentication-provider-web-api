using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Apps;

public interface IAppRepository
{
    Task<App> GetByIdAsync(string id);

    Task<List<App>> GetByIdsAsync(List<string> ids);

    Task<bool> ExistsByNameInTenantAsync(
        string name,
        string tenantId);

    Task CreateAndSaveAsync(App app);

    Task<Pagination<App>> GetAllAsync(
        string tenantId,
        int page,
        int pageSize);
}
