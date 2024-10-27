using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.Exceptions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.UnitOfWork.EfCore.Extensions;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

internal sealed class AccountRepository(
AuthDbContext context,
[FromKeyedServices(MapperKeyedService.DataAccess)] IMapper _mapper
)
: AuthDbContextRepository<AccountEfCore>(context),
IAccountRepository
{
public async Task CreateAsync(Account account)
{
    var accountEfCore = new AccountEfCore(account);

    var accountRolesEfCore = account
        .Roles
        .ConvertAll(r => new AccountRole(account.Id, r.Id));

    var accountAppsEfCore = account
        .Apps
        .ConvertAll(a => new AccountApp(account.Id, a.Id));

    await Entities
        .AddAsync(accountEfCore)
        .ConfigureAwait(false);

    await BaseContext
        .AddRangeAsync(accountRolesEfCore)
        .ConfigureAwait(false);

    await BaseContext
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

    return _mapper.Map<Account>(account);
}

async Task<Account> IAccountRepository.GetByIdAsync(string id)
{
    var query =
        Entities
        .Include(a => a.Roles)
            .ThenInclude(r => r.Permissions)
        .Include(a => a.Tenant)
        .Include(a => a.Apps)
        .AsNoTracking()
        .AsSplitQuery()
        .Where(a => a.Id == id);

    var account = await query
        .FirstOrDefaultAsync()
        .ConfigureAwait(false);

    if (Guard.IsNull(account))
    {
        throw new SpecificResourceNotFoundException<Account>(nameof(Account.Id), id);
    }

    return _mapper.Map<Account>(account);
}

public async Task<Account> GetByIdAsync(
    string id,
    params string[] includes)
{
    var queryInclude = InsertIncludes(
        Entities,
        [.. includes]);

    var query = queryInclude
        .Where(a => a.Id == id)
        .AsNoTracking()
        .AsSplitQuery();

    var account = await query
        .FirstOrDefaultAsync()
        .ConfigureAwait(false);

    AssertNullEntity(account, id, nameof(Account.Id));

    return _mapper.Map<Account>(account);
}

public async Task UpdateTenantByIdAndSaveAsync(
    string id,
    Tenant tenant)
{
    await UpdateAndSaveByIdAsync(id, new { TenantId = tenant.Id })
        .ConfigureAwait(false);
}

public async Task AddRoleByIdAsync(
    string id,
    string roleId)
{
    var accountRole = new AccountRole(id, roleId);

    await ConcreteContext
        .AccountsRoles
        .AddAsync(accountRole);
}

public Task RemoveRoleByIdAsync(
    string id,
    string roleId)
{
    var query = ConcreteContext
        .AccountsRoles
        .Where(a => a.AccountId == id)
        .Where(a => a.RoleId == roleId);

    BaseContext.RemoveRange(query);

    return Task.CompletedTask;
}

public async Task<Pagination<Account>> GetAllAsync(
    string tenantId,
    int page,
    int pageSize)
{
    var query = ConcreteContext
        .Accounts
        .Where(a => a.TenantId == tenantId)
        .Paginate(page, pageSize);

    var paginated = await query
        .ToPaginateAsync(page, pageSize)
        .ConfigureAwait(false);

    return _mapper.Map<Pagination<Account>>(paginated);
}
}
