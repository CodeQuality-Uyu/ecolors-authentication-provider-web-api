using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.UnitOfWork.Abstractions.Repositories;
namespace CQ.AuthProvider.BusinessLogic.Accounts;

public interface IAccountRepository
{
    Task CreateAsync(Account account);

    Task<bool> ExistByEmailAsync(string email);

    Task<Account> GetByEmailAsync(string email);

    Task<Account> GetByIdAsync(string id);

    Task<Account> GetByIdAsync(
        string id,
        params string[] includes);

    Task UpdateTenantByIdAndSaveAsync(
        string id,
        Tenant tenant);

    Task AddRoleByIdAsync(
        string id,
        string roleId);

    Task RemoveRoleByIdAsync(
        string id,
        string roleId);

    Task<Pagination<Account>> GetAllAsync(
        string tenantId,
        int page,
        int pageSize);
}
