using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Tenants;
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
            account.ProfilePictureId,
            account.Tenant);

        var accountRolesEfCore = account
            .Roles
            .ConvertAll(r => new AccountRole(account.Id, r.Id));

        var accountAppsEfCore = account
            .Apps
            .ConvertAll(a => new AccountApp(account.Id, a.Id));

        await _entities
            .AddAsync(accountEfCore)
            .ConfigureAwait(false);

        await _baseContext
            .AddRangeAsync(accountRolesEfCore)
            .ConfigureAwait(false);

        await _baseContext
            .AddRangeAsync(accountAppsEfCore)
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
        var query =
            _entities
            .Include(a => a.Roles)
            .ThenInclude(r => r.Permissions)
            .Include(a => a.Tenant)
            .Include(a => a.Apps)
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


    public async Task UpdateTenantByIdAsync(
        string id,
        Tenant tenant)
    {
        await UpdateAndSaveByIdAsync(id, new { TenantId = tenant.Id }).ConfigureAwait(false);
    }
}
