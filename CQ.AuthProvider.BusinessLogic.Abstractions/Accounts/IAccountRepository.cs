
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

public interface IAccountRepository
{
    Task CreateAsync(Account account);

    Task<bool> ExistByEmailAsync(string email);

    Task<Account> GetByEmailAsync(string email);

    Task<Account> GetByIdAsync(string id);

    Task UpdateTenantByIdAsync(
        string id,
        Tenant tenant);
}
