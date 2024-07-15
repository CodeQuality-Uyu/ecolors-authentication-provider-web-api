
using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;
using System.Security;

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
        var appsIdsOfAccountLogged = accountLogged
            .Apps
            .ConvertAll(a => a.Id);

        var query = _dbSet
            .Where(p =>
            (appId != null && p.AppId == appId) ||
            (appId == null && appsIdsOfAccountLogged.Contains(p.AppId)))
            .Where(p => isPrivate == null || p.IsPublic == !isPrivate)
            .Where(p => roleId == null || p.Roles.Exists(r => r.RoleId == roleId))
            ;

        var permissions = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Permission>>(permissions);
    }

    public async Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> permissionsKeys)
    {
        var keys = mapper.Map<List<string>>(permissionsKeys);

        var query = _dbSet
            .Where(p => keys.Contains(p.Key));

        var permissions = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Permission>>(permissions);
    }

    public async Task<bool> ExistByKeyAsync(PermissionKey permissionKey)
    {
        var key = permissionKey.ToString();

        var exist = await ExistAsync(p => p.Key == key).ConfigureAwait(false);

        return exist;
    }

    public async Task CreateAsync(Permission permission)
    {
        var permissionEfCore = new PermissionEfCore(
            permission.Id,
            permission.Name,
            permission.Description,
            permission.Key,
            permission.IsPublic,
            permission.App.Id,
            permission.Tenant.Id);

        await CreateAsync(permissionEfCore).ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(List<Permission> permissions)
    {
        var permissionsEfCore = permissions.ConvertAll(p => new PermissionEfCore(
            p.Id,
            p.Name,
            p.Description,
            p.Key,
            p.IsPublic,
            p.App.Id,
            p.Tenant.Id));

        await CreateBulkAsync(permissionsEfCore).ConfigureAwait(false);
    }
}
