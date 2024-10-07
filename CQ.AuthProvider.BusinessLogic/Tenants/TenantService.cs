using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Tenants;

internal sealed class TenantService(
    ITenantRepository _tenantRepository,
    IAccountRepository _accountRepository)
    : ITenantInternalService
{
    public async Task CreateAsync(
        CreateTenantArgs args,
        AccountLogged accountLogged)
    {
        await AssertNameAsync(args.Name)
            .ConfigureAwait(false);

        var tenant = new Tenant(
            args.Name,
            accountLogged);

        await _tenantRepository
            .CreateAndSaveAsync(tenant)
            .ConfigureAwait(false);

        await _accountRepository
            .UpdateTenantByIdAndSaveAsync(
            accountLogged.Id,
            tenant)
            .ConfigureAwait(false);
    }

    private async Task AssertNameAsync(string name)
    {
        var existTenant = await _tenantRepository
                    .ExistByNameAsync(name)
                    .ConfigureAwait(false);
        if (existTenant)
        {
            throw new InvalidOperationException("Name is in use");
        }
    }

    public async Task<Pagination<Tenant>> GetAllAsync(
        int page = 1,
        int pageSize = 10)
    {
        var tenants = await _tenantRepository
            .GetPaginatedAsync(
            page,
            pageSize)
            .ConfigureAwait(false);

        return tenants;
    }

    public async Task<Tenant> GetByIdAsync(
        string id,
        AccountLogged accountLogged)
    {
        var tenant = await _tenantRepository
            .GetByIdAsync(id)
            .ConfigureAwait(false);

        return tenant;
    }

    public async Task UpdateNameByIdAndSaveAsync(
        string id,
        string newName)
    {
        await AssertNameAsync(newName)
            .ConfigureAwait(false);

        await _tenantRepository
            .UpdateNameByIdAndSaveAsync(id, newName)
            .ConfigureAwait(false);
    }
}
