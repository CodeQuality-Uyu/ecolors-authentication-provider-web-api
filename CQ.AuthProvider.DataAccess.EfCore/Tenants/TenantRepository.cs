using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore.Tenants;

internal sealed class TenantRepository
    (AuthDbContext context,
    IMapper mapper)
    : AuthDbContextRepository<TenantEfCore>(context),
    ITenantRepository
{
    public async Task CreateAndSaveAsync(Tenant tenant)
    {
        var tenantEfCore = mapper.Map<TenantEfCore>(tenant);

        await CreateAndSaveAsync(tenantEfCore).ConfigureAwait(false);
    }

    public async Task<bool> ExistByNameAsync(string name)
    {
        var exist = await ExistAsync(t => t.Name == name).ConfigureAwait(false);

        return exist;
    }

    public async Task<Pagination<Tenant>> GetPaginatedAsync(
        int page = 1,
        int pageSize = 10)
    {
        var tenants = await GetPagedAsync(null, page, pageSize).ConfigureAwait(false);

        return mapper.Map<Pagination<Tenant>>(tenants);
    }

    async Task<Tenant> ITenantRepository.GetByIdAsync(string id)
    {
        var tenant = await GetByIdAsync(id).ConfigureAwait(false);

        return mapper.Map<Tenant>(tenant);
    }

    public async Task UpdateOwnerByIdAsync(
        string id,
        Account newOwner)
    {
        var tenant = await GetByIdAsync(id).ConfigureAwait(false);

        tenant.OwnerId = newOwner.Id;
    }

    public async Task UpdateNameByIdAndSaveAsync(
        string id,
        string newName)
    {
        var tenant = await GetByIdAsync(id).ConfigureAwait(false);

        tenant.Name = newName;

        await _baseContext
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }
}
