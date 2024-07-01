namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

public interface IAccountService
{
    Task<CreateAccountResult> CreateAsync(CreateAccountArgs auth);

    Task<AccountLogged> GetByTokenAsync(string token);

    Task<Account> GetByEmailAsync(string email);

    Task<Account> GetByIdAsync(string id);
}

internal interface IAccountInternalService : IAccountService
{
}
