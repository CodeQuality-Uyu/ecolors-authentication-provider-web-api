using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles.Exceptions;
using CQ.Exceptions;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

internal sealed class RoleService(
    IRoleRepository roleRepository,
    IPermissionRepository permissionRepository,
    IAppInternalService appInternalService)
    : IRoleInternalService
{
    public async Task<List<Role>> GetAllAsync(
        bool? isPrivate,
        AccountLogged accountLogged)
    {
        if (isPrivate == null || isPrivate.Value)
        {
            var hasPermission = accountLogged.HasPermission(PermissionKey.GetAllPrivateRoles);

            if (isPrivate == null)
            {
                isPrivate = hasPermission ? isPrivate : false;
            }
            else if (!hasPermission)
            {

                throw new AccessDeniedException(PermissionKey.GetAllPrivateRoles.ToString());
            }
        }

        var permissions = await roleRepository
            .GetAllAsync(
            isPrivate,
            accountLogged)
            .ConfigureAwait(false);

        return permissions;
    }

    public async Task AssertByKeyAsync(RoleKey key)
    {
        var existRoleKey = await roleRepository
            .ExistByKeyAsync(key)
            .ConfigureAwait(false);

        if (existRoleKey)
        {
            throw new SpecificResourceNotFoundException<Role>(nameof(Role.Key), key.ToString());
        }
    }

    public async Task CreateAsync(
        CreateRoleArgs args,
        AccountLogged accountLogged)
    {
        await AssertByKeyAsync(args.Key).ConfigureAwait(false);

        if (args.IsDefault)
        {
            await roleRepository
                .RemoveDefaultAsync()
                .ConfigureAwait(false);
        }

        var permissions = await permissionRepository
            .GetAllByKeysAsync(args.PermissionKeys)
            .ConfigureAwait(false);

        var app = await appInternalService
            .GetByIdAsync(args.AppId)
            .ConfigureAwait(false);

        var role = new Role(
            args.Name,
            args.Description,
            args.IsPublic,
            args.Key,
            permissions,
            args.IsDefault,
            app);

        await roleRepository
            .CreateAsync(
            role,
            accountLogged)
            .ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(
        List<CreateRoleArgs> args,
        AccountLogged accountLogged)
    {
        var rolesKeys = args
            .Select(p => p.Key)
            .Distinct()
            .ToList();

        if (rolesKeys.Count != args.Count)
        {
            var duplicatedKeys = args
                .GroupBy(r => r.Key)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key.ToString())
                .ToList();

            throw new SpecificResourceDuplicatedException<Role>([nameof(Role.Key)], duplicatedKeys);
        }

        var defaultRoles = args
            .GroupBy(r => r.IsDefault)
            .Count(g => g.Count() > 1);
        if (defaultRoles > 1)
        {
            throw new SpecificResourceDuplicatedException<Role>(nameof(Role.IsDefault), "true");
        }

        if (defaultRoles == 1)
        {
            await roleRepository
                .RemoveDefaultAsync()
                .ConfigureAwait(false);
        }

        await AssertByKeysAsync(rolesKeys).ConfigureAwait(false);

        var allPermissionsKeyes = args
            .SelectMany(a => a.PermissionKeys)
            .ToList();

        var permissions = await permissionRepository
            .GetAllByKeysAsync(allPermissionsKeyes)
            .ConfigureAwait(false);

        if (permissions.Count != allPermissionsKeyes.Count)
        {
            var missingPermissions = allPermissionsKeyes
                .Where(p => !permissions.Exists(pp => pp.Key == p))
                .ToList();
            var missingPermissionsMapped = missingPermissions.ConvertAll(p => p.ToString());

            throw new SpecificResourceNotFoundException<Permission>([nameof(Permission.Key)], missingPermissionsMapped);
        }

        var allAppsIds = args.ConvertAll(r => r.AppId);

        var apps = await appInternalService
            .GetAllByIdAsync(allAppsIds)
            .ConfigureAwait(false);
        if(apps.Count != allAppsIds.Count)
        {
            var missingApps = allAppsIds
                .Where(a => !apps.Exists(aa => aa.Id == a))
                .ToList();

            throw new SpecificResourceNotFoundException<App>([nameof(App.Id)], missingApps);
        }

        var roles = args
            .ConvertAll(r =>
            {
                var app = apps.First(a => a.Id == r.AppId);

                return new Role(
                r.Name,
                r.Description,
                r.IsPublic,
                r.Key,
                permissions,
                r.IsDefault,
                app);
            });

        await roleRepository
            .CreateBulkAsync(
            roles,
            accountLogged)
            .ConfigureAwait(false);
    }

    private async Task AssertByKeysAsync(List<RoleKey> rolesKeyes)
    {
        var rolesSaved = await roleRepository
            .GetAllByRolesKeyesAsync(rolesKeyes)
            .ConfigureAwait(false);

        if (rolesSaved.Count != rolesKeyes.Count)
        {
            var missedKeyes = rolesKeyes
                .Where(pk => !rolesSaved.Exists(p => p.Key == pk))
                .ToList();
            var simpleMissedKeyes = missedKeyes.ConvertAll(r => r.ToString());

            throw new SpecificResourceDuplicatedException<Role>([nameof(Role.Key)], simpleMissedKeyes);
        }
    }

    public async Task<bool> HasPermissionAsync(
        List<RoleKey> rolesKeyes,
        PermissionKey permissionKey)
    {
        var existPermission = await roleRepository
            .HasPermissionAsync(rolesKeyes,
            permissionKey)
            .ConfigureAwait(false);

        return existPermission;
    }

    public async Task AddPermissionByIdAsync(
        string id,
        AddPermissionArgs args)
    {
        var duplicatedPermissions = await roleRepository
            .FilterDuplicatedPermissionsAsync(
            id,
            args.PermissionsKeys)
            .ConfigureAwait(false);

        if (duplicatedPermissions.Count != 0)
        {
            throw new PermissionsDuplicatedException(duplicatedPermissions);
        }

        await roleRepository
            .AddPermissionsAsync(
            id,
            args.PermissionsKeys)
            .ConfigureAwait(false);
    }

    public async Task<Role> GetByKeyAsync(RoleKey key)
    {
        var role = await roleRepository
            .GetByKeyAsync(key)
            .ConfigureAwait(false);

        return role;
    }

    public async Task<Role> GetDefaultAsync()
    {
        var roleDefault = await roleRepository
            .GetDefaultAsync()
            .ConfigureAwait(false);

        return roleDefault;
    }
}
