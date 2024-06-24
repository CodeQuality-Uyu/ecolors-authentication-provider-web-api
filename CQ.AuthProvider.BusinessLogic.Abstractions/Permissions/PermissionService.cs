using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.Exceptions;
using CQ.Utility;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

internal abstract class PermissionService(
    IPermissionRepository permissionRepository)
    : IPermissionInternalService
{
    public async Task<List<Permission>> GetAllAsync(
        bool? isPrivate,
        string? roleId,
        Account accountLogged)
    {
        if (isPrivate.HasValue && isPrivate.Value)
        {
            accountLogged.AssertPermission(PermissionKey.GetAllPrivatePermissions);
        }

        if (Guard.IsNotNullOrEmpty(roleId))
        {
            accountLogged.AssertPermission(PermissionKey.GetAllPermissionsByRoleId);
        }

        var permissions = await permissionRepository
            .GetAllAsync(
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

    public async Task CreateAsync(CreatePermissionArgs args)
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

        var permission = new Permission(
            args.Name,
            args.Description,
            args.IsPublic,
            args.Key);

        await permissionRepository
            .CreateAsync(permission)
            .ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(List<CreatePermissionArgs> args)
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

        var permissions = args.ConvertAll(p => new Permission(
            p.Name,
            p.Description,
            p.IsPublic,
            p.Key));

        await permissionRepository
            .CreateBulkAsync(permissions)
            .ConfigureAwait(false);
    }
}
