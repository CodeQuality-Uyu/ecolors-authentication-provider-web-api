using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;

public interface ITenantRepository
{
    Task<bool> ExistByNameAsync(string name);

    Task CreateAndSaveAsync(Tenant tenant);

    Task<Pagination<Tenant>> GetPaginatedAsync(
        int page = 10,
        int pageSize = 10);

    Task<Tenant> GetByIdAsync(string id);
}
