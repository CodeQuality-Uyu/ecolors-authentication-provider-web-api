namespace CQ.AuthProvider.BusinessLogic.Accounts;

public interface IAccountService
{
    Task<CreateAccountResult> CreateAndSaveAsync(CreateAccountArgs auth);

    Task UpdatePasswordByCredentialsAsync(UpdatePasswordArgs args);

    Task<Account> GetByEmailAsync(string email);
}

internal interface IAccountInternalService
    : IAccountService
{
    Task<CreateAccountResult> CreateAsync(
        Account account,
        string password);

    Task AssertByEmailAsync(string email);
}
