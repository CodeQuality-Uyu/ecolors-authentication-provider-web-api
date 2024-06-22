
namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

internal interface IAccountRepository
{
    Task CreateAsync(Account account);

    Task<bool> ExistByEmailAsync(string email);

    Task<Account> GetByEmailAsync(string id);

    Task<Account> GetByIdAsync(string id);
}
