using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Accounts;

public interface IAccountService
{
    Task<CreateAccountResult> CreateAndSaveAsync(CreateAccountArgs args);
    
    Task<CreateAccountResult> CreateAndSaveAsync(
        CreateAccountForArgs args,
        AccountLogged accountLogged);

    Task UpdatePasswordByCredentialsAsync(UpdatePasswordArgs args);

    Task<Account> GetByEmailAsync(string email);

    Task<Pagination<Account>> GetAllAsync(
        int page,
        int pageSize,
        AccountLogged accountLogged);
}

internal interface IAccountInternalService
    : IAccountService
{
    Task<CreateAccountResult> CreateIdentityAndSaveAsync(
        Account account,
        string password);

    Task AssertByEmailAsync(string email);
}
