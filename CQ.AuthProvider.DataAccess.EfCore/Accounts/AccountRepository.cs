using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.Exceptions;
using CQ.UnitOfWork.EfCore.Core;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

internal sealed class AccountRepository(
    AuthDbContext context,
    IMapper mapper
    )
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
            account.ProfilePictureUrl,
            account.Apps,
            account.Tenant);

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
        var query =
            _dbSet
            .Include(a => a.Roles)
            .ThenInclude(r => r.Role)
            .ThenInclude(r => r.Permissions)
            .ThenInclude(p => p.Permission)
            .Include(a => a.Tenant)
            .Include(a => a.Apps)
            .ThenInclude(a => a.App)
            .Where(a => a.Id == id);

        var account = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        if (Guard.IsNull(account))
        {
            throw new SpecificResourceNotFoundException<Account>(nameof(Account.Id), id);
        }

        return mapper.Map<Account>(account);
    }
}
