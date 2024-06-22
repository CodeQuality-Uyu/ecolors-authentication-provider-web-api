namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

public interface IAccountService
{
    Task<CreateAccountResult> CreateAsync(CreateAccountArgs auth);

    Task<Account> GetByTokenAsync(string token);

    Task<Account> GetByEmailAsync(string email);
}

internal interface IAccountInternalService : IAccountService
{
    Task<Account> GetByIdAsync(string id);
}
