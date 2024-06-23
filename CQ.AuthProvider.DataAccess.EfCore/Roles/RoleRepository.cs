using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.UnitOfWork.EfCore.Core;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

internal sealed class RoleRepository(
    EfCoreContext context,
    IMapper mapper,
    IRepository<PermissionEfCore> permissionRepository,
    IRepository<RolePermission> rolePermissionRepository)
    : EfCoreRepository<RoleEfCore>(context),
    IRoleRepository
{
    public async Task<List<Role>> GetAllAsync(bool? isPrivate)
    {
        var query = _dbSet
            .Include(r => r.Permissions)
            .ThenInclude(p => p.Permission)
            .Where(r => isPrivate == null || r.IsPublic == !isPrivate);

        var roles = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Role>>(roles);
    }

    public async Task<bool> ExistByKeyAsync(RoleKey key)
    {
        var roleKey = key.ToString();

        var existRole = await ExistAsync(r => r.Key == roleKey).ConfigureAwait(false);

        return existRole;
    }

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
        var roleEfCore = new RoleEfCore(
            role.Id,
            role.Name,
            role.Description,
            role.Key,
            role.Permissions,
            role.IsPublic,
            role.IsDefault);

        await CreateAsync(roleEfCore).ConfigureAwait(false);
    }

    public async Task<List<Role>> GetAllByRolesKeyesAsync(List<RoleKey> rolesKeyes)
    {
        var keyes = rolesKeyes.ConvertAll(r => r.ToString());

        var query = _dbSet
           .Include(r => r.Permissions)
           .ThenInclude(p => p.Permission)
           .Where(r => keyes.Contains(r.Key));

        var roles = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Role>>(roles);
    }

    public async Task<bool> HasPermissionAsync(
        List<RoleKey> rolesKeyes,
        PermissionKey permissionKey)
    {
        var keyes = rolesKeyes.ConvertAll(r => r.ToString());
        var permissionKeyMapped = permissionKey.ToString();

        var query = _dbSet
           .Include(r => r.Permissions)
           .ThenInclude(p => p.Permission)
           .Where(r => keyes.Contains(r.Key))
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

        return mapper.Map<Role>(role);
    }

    public async Task<List<PermissionKey>> FilterDuplicatedPermissionsAsync(
        string id,
        List<PermissionKey> permissionsKeyes)
    {
        var keyes = permissionsKeyes.ConvertAll(r => r.ToString());

        var query = _dbSet
           .Include(r => r.Permissions.Where(p => !keyes.Contains(p.Permission.Key)))
           .ThenInclude(p => p.Permission)
           .Where(r => r.Id == id);

        var role = await query
            .FirstAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<PermissionKey>>(role.Permissions);
    }

    public async Task<Role> GetByKeyAsync(RoleKey key)
    {
        var keyMapped = key.ToString();

        var query = _dbSet
            .Include(r => r.Permissions)
            .ThenInclude(p => p.Permission)
            .Where(r => r.Key == keyMapped);

        var role = await query
            .FirstAsync()
            .ConfigureAwait(false);

        return mapper.Map<Role>(role);
    }

    public async Task<Role> GetDefaultAsync()
    {
        var query = _dbSet
            .Include(r => r.Permissions)
            .ThenInclude(p => p.Permission)
            .Where(r => r.IsDefault);

        var role = await query
            .FirstAsync()
            .ConfigureAwait(false);

        return mapper.Map<Role>(role);
    }

    public async Task CreateBulkAsync(List<Role> roles)
    {
        var rolesEfCore = mapper.Map<List<RoleEfCore>>(roles);

        await CreateBulkAsync(rolesEfCore).ConfigureAwait(false);
    }

    public async Task AddPermissionsAsync(string id, List<PermissionKey> permissionsKeyes)
    {
        var permissionsKeyesMapped = permissionsKeyes.ConvertAll(p => p.ToString());
        var permissions = await permissionRepository
            .GetAllAsync(p => permissionsKeyesMapped.Contains(p.Key))
            .ConfigureAwait(false);

        var rolePermissions = permissions.ConvertAll(p => new RolePermission(id, p.Id));
        
        await rolePermissionRepository
            .CreateBulkAsync(rolePermissions)
            .ConfigureAwait(false);
    }
}
