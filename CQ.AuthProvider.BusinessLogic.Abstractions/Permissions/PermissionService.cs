using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.Exceptions;
using CQ.Utility;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

internal sealed class PermissionService(
    IPermissionRepository permissionRepository,
    IAppInternalService appService)
    : IPermissionInternalService
{
    public async Task<List<Permission>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? roleId,
        AccountLogged accountLogged)
    {
        if (isPrivate == null ||
            isPrivate.Value)
        {
            var hasPermission = accountLogged.HasPermission(PermissionKey.GetAllPrivatePermissions);

            if (isPrivate == null)
            {
                isPrivate = hasPermission
                    ? null
                    : false;
            }
            else if (!hasPermission)
            {
                accountLogged.AssertPermission(PermissionKey.GetAllPermissionsByRoleId);
            }
        }

        if (Guard.IsNotNullOrEmpty(roleId))
        {
            accountLogged.AssertPermission(PermissionKey.GetAllPermissionsByRoleId);
        }

        if (Guard.IsNullOrEmpty(appId) &&
            !accountLogged.HasPermission(PermissionKey.GetAllPermissionsOfTenant))
        {
            appId = accountLogged.AppLogged.Id;
        }

        if (Guard.IsNotNullOrEmpty(appId))
        {
            accountLogged.AssertPermission(PermissionKey.GetAllPermissionsOfTenant);
        }

        var permissions = await permissionRepository
            .GetAllAsync(
            appId,
            isPrivate,
            roleId,
            accountLogged)
            .ConfigureAwait(false);

        return permissions;
    }

    public async Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> permissionKeys)
    {
        var permissionsSaved = await permissionRepository
            .GetAllByKeysAsync(permissionKeys)
            .ConfigureAwait(false);

        if (permissionsSaved.Count != permissionKeys.Count)
        {
            var missingPermissions = permissionKeys
                .Where(pk => !permissionsSaved.Exists(p => p.Key == pk))
                .ToList();
            var missingPermissionsMapped = missingPermissions.ConvertAll(p => p.ToString());

            throw new SpecificResourceNotFoundException<Permission>([nameof(Permission.Key)], missingPermissionsMapped);
        }

        return permissionsSaved;
    }

    public async Task AssertByKeysAsync(List<PermissionKey> permissionKeys)
    {
        await GetAllByKeysAsync(permissionKeys)
            .ConfigureAwait(false);
    }

    public async Task CreateAsync(
        CreatePermissionArgs args,
        AccountLogged accountLogged)
    {
        var existPermission = await permissionRepository
            .ExistByKeyAsync(args.Key)
            .ConfigureAwait(false);

        if (existPermission)
        {
            throw new SpecificResourceDuplicatedException<Permission>(
                nameof(Permission.Key),
                args.Key.ToString());
        }

        var app = accountLogged.AppLogged;
        if (Guard.IsNotNullOrEmpty(args.AppId))
        {
            app = await appService
                .GetByIdAsync(
                args.AppId!,
                accountLogged)
                .ConfigureAwait(false);
        }

        var permission = new Permission(
            args.Name,
            args.Description,
            args.IsPublic,
            args.Key,
            app);

        await permissionRepository
            .CreateAsync(permission)
            .ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(
        List<CreatePermissionArgs> args,
        AccountLogged accountLogged)
    {
        var permissionKeys = args
            .Select(p => p.Key)
            .Distinct()
            .ToList();

        if (permissionKeys.Count != args.Count)
        {
            var duplicatedKeys = args
                .GroupBy(r => r.Key)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key.ToString())
                .ToList();

            throw new SpecificResourceDuplicatedException<Permission>([nameof(Permission.Key)], duplicatedKeys);
        }

        var allPermissionsKeyes = args.ConvertAll(a => a.Key);

        var permissionsSaved = await permissionRepository
            .GetAllByKeysAsync(allPermissionsKeyes)
            .ConfigureAwait(false);

        if (permissionsSaved.Count != 0)
        {
            var permissionsSavedKeys = allPermissionsKeyes
                .Where(pk => !permissionsSaved.Exists(p => p.Key == pk))
                .ToList();
            var permissionsSavedKeysMapped = permissionsSavedKeys.ConvertAll(p => p.ToString());

            throw new SpecificResourceDuplicatedException<Permission>([nameof(Permission.Key)], permissionsSavedKeysMapped);
        }

        var appsIds = args
            .GroupBy(a => a.AppId)
            .Where(a => Guard.IsNotNullOrEmpty(a.Key))
            .Select(g => g.Key)
            .ToList();

        List<App> appsSaved = [];
        if (appsIds.Count != 0)
        {
            appsSaved = await appService
                .GetAllByIdAsync(
                appsIds,
                accountLogged)
                .ConfigureAwait(false);

            if (appsSaved.Count != appsIds.Count)
            {
                var missingAppsIds = appsIds
                    .Where(a => !appsSaved.Exists(aa => aa.Id == a))
                    .ToList();

                throw new SpecificResourceNotFoundException<App>(
                    [nameof(App.Id)],
                    missingAppsIds);
            }
        }

        var permissions = args.ConvertAll(p =>
        {
            var app = accountLogged.AppLogged;
            if (Guard.IsNotNullOrEmpty(p.AppId))
            {
                app = appsSaved.First(a => a.Id == p.AppId);
            }

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
