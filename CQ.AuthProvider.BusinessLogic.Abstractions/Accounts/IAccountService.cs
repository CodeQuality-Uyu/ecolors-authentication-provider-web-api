namespace CQ.AuthProvider.BusinessLogic.Accounts;

public interface IAccountService
{
    Task<CreateAccountResult> CreateAsync(CreateAccountArgs auth);

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
    Task<CreateAccountResult> InternalCreationAsync(CreateAccountArgs args);
}
