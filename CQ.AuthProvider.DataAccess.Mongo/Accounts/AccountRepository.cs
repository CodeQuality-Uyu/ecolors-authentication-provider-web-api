using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.UnitOfWork.MongoDriver.Core;

namespace CQ.AuthProvider.DataAccess.Mongo.Accounts;

internal sealed class AccountRepository(
    AuthDbContext context,
    IMapper mapper)
    : MongoDriverRepository<AccountMongo>(context),
    IAccountRepository
{
    public async Task CreateAsync(Account account)
    {
        var accountMongo = new AccountMongo(
            account.Id,
            account.Email,
            account.FullName,
            account.FirstName,
            account.LastName,
            account.Locale,
            account.TimeZone,
            account.Roles,
            account.ProfilePictureUrl);

        await CreateAsync(accountMongo)
            .ConfigureAwait(false);
    }

    public async Task<bool> ExistByEmailAsync(string email)
    {
        return await ExistAsync(a => a.Email == email)
            .ConfigureAwait(false);
    }

    public async Task<Account> GetByEmailAsync(string email)
    {
        var account = await GetAsync(a => a.Email == email)
            .ConfigureAwait(false);

        return mapper.Map<Account>(account);
    }

    public new async Task<Account> GetByIdAsync(string id)
    {
        var account = await GetAsync(a => a.Id == id)
            .ConfigureAwait(false);

        return mapper.Map<Account>(account);
    }
}
