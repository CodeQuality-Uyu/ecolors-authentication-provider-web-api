using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.UnitOfWork.EfCore.Core;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

internal sealed class RoleRepository(
    AuthDbContext context,
    IMapper mapper,
    IRepository<PermissionEfCore> permissionRepository,
    IRepository<RolePermission> rolePermissionRepository)
    : EfCoreRepository<RoleEfCore>(context),
    IRoleRepository
{
    public async Task<List<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? tenantId,
        AccountLogged accountLogged)
    {
        var appsIdsOfAccountLogged = accountLogged
            .Apps
            .ConvertAll(a => a.Id);

        var canSeeOfTenant = accountLogged.HasPermission(PermissionKey.GetAllRolesOfTenant);

        var query = _dbSet
            .Where(r => tenantId == null || r.TenantId == tenantId)
            .Where(r => isPrivate == null || r.IsPublic == !isPrivate)
            .Where(r =>
            (appId != null && r.Apps.Any(a => a.AppId == appId)) ||
            (appId == null && (tenantId == null || canSeeOfTenant || r.Apps.Any(a => appsIdsOfAccountLogged.Contains(a.AppId)))))
            ;

        var roles = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Role>>(roles);
    }

    //public async Task<bool> ExistByKeyAsync(RoleKey key)
    //{
    //    var roleKey = key.ToString();

    //    var existRole = await ExistAsync(r => r.Key == roleKey).ConfigureAwait(false);

    //    return existRole;
    //}

    public async Task RemoveDefaultAsync()
    {
        var role = await GetAsync(r => r.IsDefault).ConfigureAwait(false);

        if (Guard.IsNull(role))
        {
            return;
        }

        role.IsDefault = false;

        await UpdateAsync(role).ConfigureAwait(false);
    }

    public async Task CreateAsync(Role role)
    {
        var roleEfCore = new RoleEfCore(role);

        await CreateAsync(roleEfCore).ConfigureAwait(false);
    }

    //public async Task<List<Role>> GetAllByRolesKeyesAsync(List<RoleKey> rolesKeys)
    //{
    //    var keys = mapper.Map<List<string>>(rolesKeys);

    //    var query = _dbSet
    //       .Include(r => r.Permissions)
    //       .ThenInclude(p => p.Permission)
    //       .Where(r => keys.Contains(r.Key));

    //    var roles = await query
    //        .ToListAsync()
    //        .ConfigureAwait(false);

    //    return mapper.Map<List<Role>>(roles);
    //}

    public async Task<bool> HasPermissionAsync(
        List<string> ids,
        PermissionKey permissionKey)
    {
        var permissionKeyMapped = permissionKey.ToString();

        var query = _dbSet
           .Include(r => r.Permissions)
           .ThenInclude(p => p.Permission)
           .Where(r => ids.Contains(r.Id))
           .Where(r => r.Permissions.Any(rp => rp.Permission.Key == permissionKeyMapped));

        var roles = await query
            .CountAsync()
            .ConfigureAwait(false);

        return roles != 0;
    }

    public new async Task<Role> GetByIdAsync(string id)
    {
        var query = _dbSet
           .Include(r => r.Permissions)
           .ThenInclude(p => p.Permission)
           .Where(r => r.Id == id);

        var role = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        Guard.ThrowIsNull(role, nameof(role));

        return mapper.Map<Role>(role);
    }

    public async Task<List<PermissionKey>> FilterDuplicatedPermissionsAsync(
        string id,
        List<PermissionKey> permissionsKeys)
    {
        var keys = mapper.Map<List<string>>(permissionsKeys);

        var query = _dbSet
           .Include(r => r.Permissions.Where(p => !keys.Contains(p.Permission.Key)))
           .ThenInclude(p => p.Permission)
           .Where(r => r.Id == id);

        var role = await query
            .FirstAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<PermissionKey>>(role.Permissions);
    }

    public async Task<Role> GetDefaultAsync()
    {
        var query = _dbSet
            .Include(r => r.Permissions)
            .ThenInclude(p => p.Permission)
            .Where(r => r.IsDefault);

        var role = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        Guard.ThrowIsNull(role, nameof(role));

        return mapper.Map<Role>(role);
    }

    public async Task CreateBulkAsync(List<Role> roles)
    {
        var rolesEfCore = roles.ConvertAll(r => new RoleEfCore(r));

        await CreateBulkAsync(rolesEfCore).ConfigureAwait(false);
    }

    public async Task AddPermissionsAsync(string id, List<PermissionKey> permissionsKeys)
    {
        var permissionsKeysMapped = mapper.Map<List<string>>(permissionsKeys);
        var permissions = await permissionRepository
            .GetAllAsync(p => permissionsKeysMapped.Contains(p.Key))
            .ConfigureAwait(false);

        var rolePermissions = permissions.ConvertAll(p => new RolePermission(id, p.Id));

        await rolePermissionRepository
            .CreateBulkAsync(rolePermissions)
            .ConfigureAwait(false);
    }
}
