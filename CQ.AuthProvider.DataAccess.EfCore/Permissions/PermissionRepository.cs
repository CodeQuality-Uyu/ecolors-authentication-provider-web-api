using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions;

internal sealed class PermissionRepository(
    AuthDbContext context,
    IMapper mapper)
    : EfCoreRepository<PermissionEfCore>(context),
    IPermissionRepository
{
    public async Task<List<Permission>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? roleId,
        string? tenantId,
        AccountLogged accountLogged)
    {
        var appsIdsOfAccountLogged = accountLogged.AppsIds;
        var rolesIds = accountLogged.RolesIds;

        var canReadOfTenant = accountLogged.HasPermission(PermissionKey.CanReadPermissionsOfTenant);
        var hasFullAccess = accountLogged.HasPermission(PermissionKey.FullAccess);

        var fullAccessKey = PermissionKey.FullAccess;

        var query = _entities
            .Where(p => tenantId == null || p.TenantId == null || p.TenantId == tenantId)
            .Where(p => isPrivate == null || p.IsPublic == !isPrivate)
            .Where(p =>
            (roleId == null && (canReadOfTenant || hasFullAccess)) ||
            (roleId != null && p.Roles.Any(r => r.Id == roleId)) ||
            p.Roles.Any(r => rolesIds.Contains(r.Id)))
            .Where(p =>
            (appId == null && (canReadOfTenant || hasFullAccess)) ||
            (appId != null && p.AppId == appId) ||
            appsIdsOfAccountLogged.Contains(p.AppId))
            .Where(p => p.Key != fullAccessKey || hasFullAccess)
            ;

        var permissions = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Permission>>(permissions);
    }

    public async Task<List<Permission>> GetAllByKeysAsync(
        List<(string appId, List<string> keys)> keys,
        AccountLogged accountLogged)
    {
        var query = _entities
            .Where(p => keys.Any(i => i.appId == p.AppId && i.keys.Contains(p.Key)))
            .Where(p => p.TenantId == accountLogged.Tenant.Id);

        var permissions = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Permission>>(permissions);
    }

    public async Task CreateBulkAsync(List<Permission> permissions)
    {
        var permissionsEfCore = permissions.ConvertAll(p => new PermissionEfCore(p));

        await CreateBulkAsync(permissionsEfCore).ConfigureAwait(false);
    }
}
