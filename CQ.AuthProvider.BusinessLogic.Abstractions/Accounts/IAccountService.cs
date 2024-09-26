namespace CQ.AuthProvider.BusinessLogic.Accounts;

public interface IAccountService
{
    Task<CreateAccountResult> CreateAndSaveAsync(CreateAccountArgs auth);

    Task<AccountLogged> GetByTokenAsync(string token);

    Task<Account> GetByEmailAsync(string email);

    Task<Account> GetByIdAsync(string id);

    Task UpdateAsync(
        UpdatePasswordArgs args,
        AccountLogged accountLogged);
}

internal interface IAccountInternalService
    : IAccountService
{
    Task<CreateAccountResult> CreateAsync(
        Account account,
        string password);

    Task AssertByEmailAsync(string email);
}
