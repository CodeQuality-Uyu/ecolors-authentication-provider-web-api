using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.UnitOfWork.EfCore.Core;
using CQ.UnitOfWork.EfCore.Extensions;
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
    public async Task<Pagination<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var appsIdsOfAccountLogged = accountLogged.AppsIds;

        var canReadOfTenant = accountLogged.HasPermission(PermissionKey.CanReadRolesOfTenant);

        var query = _entities
            .Where(r => r.TenantId == accountLogged.Tenant.Id)
            .Where(r => isPrivate == null || r.IsPublic == !isPrivate)
            .Where(r =>
            (appId == null && canReadOfTenant) ||
            (appId != null && r.AppId == appId) ||
            appsIdsOfAccountLogged.Contains(r.AppId)
            );

        var roles = await query
            .PaginateAsync(null, page, pageSize)
            .ConfigureAwait(false);

        return mapper.Map<Pagination<Role>>(roles);
    }

    public async Task RemoveDefaultsAndSaveAsync(
        List<string> appsIds,
        AccountLogged accountLogged)
    {
        var query = _entities
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

        _entities.UpdateRange(roles);
        await _baseContext
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }

    public new async Task<Role> GetByIdAsync(string id)
    {
        var query = _entities
           .Include(r => r.Permissions)
           .Where(r => r.Id == id);

        var role = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        Guard.ThrowIsNull(role, nameof(role));

        return mapper.Map<Role>(role);
    }

    public async Task<Role> GetDefaultAsync()
    {
        var query = _entities
            .Include(r => r.Permissions)
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

        await CreateBulkAsync(rolesEfCore)
            .ConfigureAwait(false);
    }

    public async Task AddPermissionsAsync(string id, List<string> permissionsKeys)
    {
        var permissions = await permissionRepository
            .GetAllAsync(p => permissionsKeys.Contains(p.Key))
            .ConfigureAwait(false);

        var rolePermissions = permissions.ConvertAll(p => new RolePermission(id, p.Id));

        await rolePermissionRepository
            .CreateBulkAsync(rolePermissions)
            .ConfigureAwait(false);
    }
}
