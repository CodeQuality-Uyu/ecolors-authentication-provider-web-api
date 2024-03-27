namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public interface IAccountService
    {
        Task<CreateAccountResult> CreateAsync(CreateAccount auth);

        Task<Account> GetByTokenAsync(string token);

        Task<Account> GetByEmailAsync(string email);
    }

    internal interface IAccountInternalService
    {
        Task<Account> GetByIdAsync(string id);
    }
}
