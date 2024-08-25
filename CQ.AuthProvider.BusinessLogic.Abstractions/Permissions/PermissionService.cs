using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.Utility;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

internal sealed class PermissionService(IPermissionRepository permissionRepository)
    : IPermissionInternalService
{
    public async Task<List<Permission>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? roleId,
        string? tenantId,
        AccountLogged accountLogged)
    {
        AssertCanReadPrivatePermissionsOrSetDefault(
            ref isPrivate,
            accountLogged);

        AssertCanFilterPermissionsByRoleIdOrSetDefault(
            ref roleId,
            accountLogged);

        AssertCanFilterPermissionsByAppIdOrSetDefault(
            ref appId,
            accountLogged);

        AssertCanFilterPermissionsByTenantIdOrSetDefault(
            ref tenantId,
            accountLogged);

        var permissions = await permissionRepository
            .GetAllAsync(
            appId,
            isPrivate,
            roleId,
            tenantId,
            accountLogged)
            .ConfigureAwait(false);

        return permissions;
    }

    private static void AssertCanReadPrivatePermissionsOrSetDefault(
        ref bool? isPrivate,
        AccountLogged accountLogged)
    {
        if (IsPrivate(isPrivate))
        {
            accountLogged.AssertPermission(PermissionKey.CanReadPrivatePermissions);
        }

        if (Guard.IsNull(isPrivate))
        {
            var hasPermission = accountLogged.HasPermission(PermissionKey.CanReadPrivatePermissions);

            isPrivate = hasPermission
                    ? null
                    : false;
        }
    }

    public static bool IsPrivate(bool? isPrivate)
    {
        return isPrivate.HasValue && isPrivate.Value;
    }

    private static void AssertCanFilterPermissionsByRoleIdOrSetDefault(
        ref string? roleId,
        AccountLogged accountLogged)
    {
        if (Guard.IsNullOrEmpty(roleId))
        {
            return;
        }

        if (!accountLogged.RolesIds.Contains(roleId!) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            accountLogged.AssertPermission(PermissionKey.CanReadPermissionsOfTenant);
        }
    }

    private static void AssertCanFilterPermissionsByAppIdOrSetDefault(
        ref string? appId,
        AccountLogged accountLogged)
    {
        if (Guard.IsNotNullOrEmpty(appId) &&
            !accountLogged.AppsIds.Contains(appId!) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            accountLogged.AssertPermission(PermissionKey.CanReadPermissionsOfTenant);
        }

        if (Guard.IsNullOrEmpty(appId) &&
            !accountLogged.HasPermission(PermissionKey.CanReadPermissionsOfTenant) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            appId = accountLogged.AppLogged.Id;
        }
    }

    private static void AssertCanFilterPermissionsByTenantIdOrSetDefault(
        ref string? tenantId,
        AccountLogged accountLogged)
    {
        if (Guard.IsNotNullOrEmpty(tenantId) &&
            accountLogged.Tenant.Id != tenantId)
        {
            accountLogged.AssertPermission(PermissionKey.FullAccess);
        }

        if (Guard.IsNullOrEmpty(tenantId) &&
            !accountLogged.HasPermission(PermissionKey.CanReadPermissionsOfTenant) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            tenantId = accountLogged.Tenant.Id;
        }
    }

    public async Task<List<Permission>> GetExactAllByKeysAsync(
        List<(string appId, List<string> keys)> keys,
        AccountLogged accountLogged)
    {
        var permissionsSaved = await permissionRepository
            .GetAllByKeysAsync(
            keys,
            accountLogged)
            .ConfigureAwait(false);

        if (permissionsSaved.Count != keys.Count)
        {
            var missingPermissions = keys
                .SelectMany(k => k.keys)
                .Where(pk => !permissionsSaved.Exists(p => p.Key == pk))
                .ToList();

            throw new InvalidOperationException($"The following permissions do not exist: {string.Join(",", missingPermissions)}");
        }

        return permissionsSaved;
    }

    public async Task CreateAsync(
        CreatePermissionArgs args,
        AccountLogged accountLogged)
    {
        await CreateBulkAsync(
            [args],
            accountLogged)
            .ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(
        List<CreatePermissionArgs> args,
        AccountLogged accountLogged)
    {
        var duplicatedKeys = args
            .GroupBy(i => i.Key)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        if (duplicatedKeys.Count != 0)
        {
            throw new ArgumentException($"Duplicated keys argument found ${string.Join(",", duplicatedKeys)}");
        }

        var allPermissionsKeys = args
            .ConvertAll(a => (a.AppId ?? accountLogged.AppLogged.Id, a.Key))
            .GroupBy(k => k.Item1)
            .Select(g => (g.Key, g.Select(i => i.Key).ToList()))
            .ToList();

        var duplicatedPermissions = await permissionRepository
            .GetAllByKeysAsync(
            allPermissionsKeys,
            accountLogged)
            .ConfigureAwait(false);
        if (duplicatedPermissions.Count != 0)
        {
            var onlyPermissionsKeys = allPermissionsKeys
                .SelectMany(g => g.Item2)
                .ToList();
            var permissionsSavedKeys = onlyPermissionsKeys
                .Where(pk => duplicatedPermissions.Exists(p => p.Key == pk))
                .ToList();

            throw new InvalidOperationException($"Duplicated keys exist ${string.Join(",", permissionsSavedKeys)}");
        }

        var appsIds = args
            .GroupBy(a => a.AppId)
            .Where(a => Guard.IsNotNullOrEmpty(a.Key))
            .Select(g => g.Key!)
            .ToList();

        if (appsIds.Count != 0)
        {
            var validAppsIds = accountLogged.AppsIds;

            var invalidAppsIds = appsIds
                .Where(id => !validAppsIds.Contains(id))
                .ToList();

            throw new InvalidOperationException($"Invalid apps ids {string.Join(",", invalidAppsIds)}");
        }

        var permissions = args.ConvertAll(p =>
        {
            var app = Guard.IsNotNullOrEmpty(p.AppId)
            ? accountLogged.Apps.First(a => a.Id == p.AppId)
            : accountLogged.AppLogged;

            return new Permission(
            p.Name,
            p.Description,
            p.IsPublic,
            p.Key,
            app);
        });

        await permissionRepository
            .CreateBulkAsync(permissions)
            .ConfigureAwait(false);
    }
}
