using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.UnitOfWork.EfCore;

namespace CQ.AuthProvider.BusinessLogic.Accounts;

internal sealed class AccountRepository(
    IMapper mapper,
    EfCoreContext context)
    : EfCoreRepository<AccountEfCore>(context),
    IAccountRepository
{
    public async Task CreateAsync(Account account)
    {
        var accountEfCore = new AccountEfCore(
            account.Id,
            account.Email,
            account.FullName,
            account.FirstName,
            account.LastName,
            account.Locale,
            account.TimeZone,
            account.Roles,
            account.ProfilePictureUrl);

        await CreateAsync(accountEfCore).ConfigureAwait(false);
    }

    public async Task<bool> ExistByEmailAsync(string email)
    {
        return await ExistAsync(a => a.Email == email).ConfigureAwait(false);
    }

    public async Task<Account> GetByEmailAsync(string email)
    {
        var account = await GetAsync(a => a.Email == email).ConfigureAwait(false);

        return mapper.Map<Account>(account);
    }

    public new async Task<Account> GetByIdAsync(string id)
    {
        var account = await GetAsync(a => a.Id == id).ConfigureAwait(false);

        return mapper.Map<Account>(account);
    }
}
