using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.UnitOfWork.EfCore.Core;
using CQ.UnitOfWork.EfCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

internal sealed class RoleRepository(
    AuthDbContext _context,
    [FromKeyedServices(MapperKeyedService.DataAccess)] IMapper _mapper,
    IRepository<PermissionEfCore> _permissionRepository,
    IRepository<RolePermission> _rolePermissionRepository)
    : EfCoreRepository<RoleEfCore>(_context),
    IRoleRepository
{
    public async Task<Pagination<Role>> GetAllAsync(
        Guid? appId,
        bool? isPrivate,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var query = Entities
            .Include(r => r.App)
            .Where(r => r.TenantId == accountLogged.Tenant.Id)
            .Where(r => isPrivate == null || r.IsPublic == !isPrivate)
            .Where(r => appId == null || r.AppId == appId)
            .AsNoTracking()
            .AsSplitQuery();

        var roles = await query
            .ToPaginateAsync(page, pageSize)
            .ConfigureAwait(false);

        return _mapper.Map<Pagination<Role>>(roles);
    }

    public async Task RemoveDefaultsAndSaveAsync(
        List<Guid> appsIds,
        AccountLogged accountLogged)
    {
        var query = Entities
            .Where(r => r.TenantId == accountLogged.Tenant.Id)
            .Where(r => appsIds.Contains(r.AppId))
            .Where(r => r.IsDefault);

        var roles = await query
            .ToListAsync()
            .ConfigureAwait(false);

        roles.ForEach(a =>
        {
            a.IsDefault = false;
        });

        Entities.UpdateRange(roles);
        await BaseContext
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }

    /*
     * Include used in: RoleService
     */
    public new async Task<Role> GetByIdAsync(Guid id)
    {
        var query = Entities
           .Include(r => r.Permissions)
           .Where(r => r.Id == id);

        var role = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        AssertNullEntity(role, id, nameof(Role.Id));

        return _mapper.Map<Role>(role);
    }

    public async Task<Role> GetDefaultByTenantIdAsync(
        Guid appId,
        Guid tenantId)
    {
        var query = Entities
            .Where(r => r.AppId == appId)
            .Where(r => r.TenantId == tenantId)
            .Where(r => r.IsDefault);

        var role = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        AssertNullEntity(role, tenantId, nameof(Role.Tenant));

        return _mapper.Map<Role>(role);
    }

    public async Task CreateBulkAsync(List<Role> roles)
    {
        var rolesEfCore = roles.ConvertAll(r => new RoleEfCore(r));

        await CreateBulkAndSaveAsync(rolesEfCore)
            .ConfigureAwait(false);
    }

    public async Task AddPermissionsAsync(
        Guid id,
        List<string> permissionsKeys)
    {
        var permissions = await _permissionRepository
            .GetAllAsync(p => permissionsKeys.Contains(p.Key))
            .ConfigureAwait(false);

        var rolePermissions = permissions.ConvertAll(p => new RolePermission
        {
            RoleId = id,
            PermissionId = p.Id
        });

        await _rolePermissionRepository
            .CreateBulkAndSaveAsync(rolePermissions)
            .ConfigureAwait(false);
    }

    public async Task<Role?> GetDefaultOrDefaultByAppIdAndTenantIdAsync(
        Guid appId,
        Guid tenantId)
    {
        var query = Entities
            .Where(r => r.AppId == appId)
            .Where(r => r.TenantId == tenantId)
            .Where(r => r.IsDefault);

        var entity = await query
            .AsNoTracking()
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        return _mapper.Map<Role>(entity);
    }

    public async Task<List<Role>> GetAllByIdsAsync(
        List<Guid> ids,
        AccountLogged accountLogged)
    {

        var query = Entities
            .Where(r => r.TenantId == accountLogged.Tenant.Id)
            .Where(r => accountLogged.AppsIds.Any(a => a == r.AppId))
            .Where(r => ids.Any(i => i == r.Id));

        var roles = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return _mapper.Map<List<Role>>(roles);
    }

    public async Task<List<Role>> GetByIdAsync(
        List<Guid> ids,
        List<Guid> appIds,
        Guid tenantId)
    {
        var query = Entities
            .Where(r => ids.Contains(r.Id) || r.IsDefault)
            .Where(r => appIds.Contains(r.AppId))
            .Where(r => r.TenantId == tenantId)
            ;

        var roles = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return _mapper.Map<List<Role>>(roles);
    }
}
