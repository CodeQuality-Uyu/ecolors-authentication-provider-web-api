using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.Utility;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Permissions;

internal sealed class PermissionService(IPermissionRepository permissionRepository)
    : IPermissionInternalService
{
    public async Task<Pagination<Permission>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? roleId,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var permissions = await permissionRepository
            .GetAllAsync(
            appId,
            isPrivate,
            roleId,
            page,
            pageSize,
            accountLogged)
            .ConfigureAwait(false);

        return permissions;
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
            .CreateBulkAndSaveAsync(permissions)
            .ConfigureAwait(false);
    }
}
