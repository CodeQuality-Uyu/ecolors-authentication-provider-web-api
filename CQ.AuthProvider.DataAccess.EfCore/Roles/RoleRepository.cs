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
    AuthDbContext context,
    [FromKeyedServices(MapperKeyedService.DataAccess)] IMapper mapper,
    IRepository<PermissionEfCore> permissionRepository,
    IRepository<RolePermission> rolePermissionRepository)
    : EfCoreRepository<RoleEfCore>(context),
    IRoleRepository
{
    public async Task<Pagination<Role>> GetAllAsync(
        Guid appId,
        bool? isPrivate,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {

        var query = Entities
            .Include(r => r.App)
            .Where(r => r.TenantId == accountLogged.Tenant.Id)
            .Where(r => isPrivate == null || r.IsPublic == !isPrivate)
            .Where(r => r.AppId == appId)
            .AsNoTracking()
            .AsSplitQuery();

        var roles = await query
            .ToPaginateAsync(page, pageSize)
            .ConfigureAwait(false);

        return mapper.Map<Pagination<Role>>(roles);
    }

    public async Task RemoveDefaultsAsync(
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

        return mapper.Map<Role>(role);
    }

    public async Task<Role> GetDefaultByTenantIdAsync(
        Guid appId,
        Guid tenantId)
    {
        var query = Entities
            // TODO check in r.AppId and inheritence app
            //.Where(r => r.AppId == appId)
            .Where(r => r.TenantId == tenantId)
            .Where(r => r.IsDefault);

        var role = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        AssertNullEntity(role, tenantId, nameof(Role.Tenant));

        return mapper.Map<Role>(role);
    }

    public async Task CreateBulkAsync(List<Role> roles)
    {
        var permissions = roles
        .SelectMany(r => r.Permissions.ConvertAll(p => new RolePermission
        {
            RoleId = r.Id,
            PermissionId = p.Id
        }));

        await BaseContext
        .AddRangeAsync(permissions)
        .ConfigureAwait(false);

        var rolesEfCore = roles.ConvertAll(r => new RoleEfCore(r));

        await Entities.AddRangeAsync(rolesEfCore).ConfigureAwait(false);
    }

    public async Task AddPermissionsAsync(
        Guid id,
        List<string> permissionsKeys)
    {
        var permissions = await permissionRepository
            .GetAllAsync(p => permissionsKeys.Contains(p.Key))
            .ConfigureAwait(false);

        var rolePermissions = permissions.ConvertAll(p => new RolePermission
        {
            RoleId = id,
            PermissionId = p.Id
        });

        await rolePermissionRepository
            .CreateBulkAndSaveAsync(rolePermissions)
            .ConfigureAwait(false);
    }

    public async Task RemovePermissionByIdAsync(
        Guid id,
        Guid permissionId)
    {
        await rolePermissionRepository
            .DeleteAndSaveAsync(rp => rp.RoleId == id && rp.PermissionId == permissionId)
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

        return mapper.Map<Role>(entity);
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

        return mapper.Map<List<Role>>(roles);
    }

    public async Task<List<Role>> GetByIdAsync(
        List<Guid> ids,
        List<Guid> appIds,
        Guid tenantId)
    {
        List<Guid> authRoles = [AuthConstants.CLIENT_OWNER_ROLE_ID, AuthConstants.APP_OWNER_ROLE_ID, AuthConstants.TENANT_OWNER_ROLE_ID];

        var query = Entities
            .Where(r => ids.Contains(r.Id))
            // TODO check in r.AppId and inheritence app
            //.Where(r => appIds.Contains(r.AppId))
            .Where(r => ids.Intersect(authRoles).Any() || r.TenantId == tenantId)
            ;

        var roles = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Role>>(roles);
    }

    public async Task DeleteAndSaveByIdAsync(Guid id)
    {
        await DeleteAndSaveAsync(r => r.Id == id)
            .ConfigureAwait(false);
    }

    public async Task<List<(Guid AppId, string RoleName)>> GetAllByAppAndNamesAsync(
    List<(Guid AppId, string RoleName)> roles)
    {
        var appIds = roles
            .Select(r => r.AppId)
            .Distinct()
            .ToList();

        var entities = await Entities
            .AsNoTracking()
            .Where(r => appIds.Contains(r.AppId))
            .Select(r => new { r.AppId, RoleName = r.Name })
            .ToListAsync()
            .ConfigureAwait(false);

        var lookup = new HashSet<(Guid AppId, string RoleName)>(roles);

        var result = entities
            .Select(r => (r.AppId, r.RoleName))
            .Where(lookup.Contains)
            .ToList();

        return result;
    }

}
