using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Tenants;

public interface ITenantRepository
{
    Task<bool> ExistByNameAsync(string name);

    Task CreateAsync(Tenant tenant);

    Task<Pagination<Tenant>> GetPaginatedAsync(
        int page = 10,
        int pageSize = 10);

    Task<Tenant> GetByIdAsync(Guid id);

    Task UpdateOwnerByIdAsync(
        Guid id,
        Account newOwner);

    Task UpdateNameByIdAndSaveAsync(
        Guid id,
        string newName);
}
