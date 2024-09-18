using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.BusinessLogic.Accounts;

public interface IAccountRepository
{
    Task CreateAndSaveAsync(Account account);

    Task<bool> ExistByEmailAsync(string email);

    Task<Account> GetByEmailAsync(string email);

    Task<Account> GetByIdAsync(string id);

    Task UpdateTenantByIdAsync(
        string id,
        Tenant tenant);
}
