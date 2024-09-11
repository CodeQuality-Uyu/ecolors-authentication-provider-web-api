using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Permissions;
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
        AccountLogged accountLogged)
    {
        var appsIdsOfAccountLogged = accountLogged.AppsIds;
        var rolesIds = accountLogged.RolesIds;

        var canReadOfTenant = accountLogged.HasPermission(PermissionKey.CanReadPermissionsOfTenant);

        var appLoggedIsAuthWebApi = accountLogged.AppLogged.Id == AuthDbContext.AUTH_WEB_API_APP_ID;

        var query = _entities
            .Where(p => (p.AppId == AuthDbContext.AUTH_WEB_API_APP_ID && appLoggedIsAuthWebApi) || p.TenantId == accountLogged.Tenant.Id)
            .Where(p => isPrivate == null || p.IsPublic == !isPrivate)
            .Where(p =>
            (roleId == null && canReadOfTenant) ||
            (roleId != null && p.Roles.Any(r => r.Id == roleId)) ||
            p.Roles.Any(r => rolesIds.Contains(r.Id)))
            .Where(p =>
            (appId == null && canReadOfTenant) ||
            (appId != null && p.AppId == appId) ||
            appsIdsOfAccountLogged.Contains(p.AppId))
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
