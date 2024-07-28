using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles.Exceptions;
using CQ.Exceptions;
using CQ.Utility;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

internal sealed class RoleService(
    IRoleRepository roleRepository,
    IPermissionRepository permissionRepository,
    IAppInternalService appService)
    : IRoleInternalService
{
    public async Task<List<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? tenantId,
        AccountLogged accountLogged)
    {
        if (Guard.IsNull(isPrivate))
        {
            var hasPermission = accountLogged.HasPermission(PermissionKey.GetAllPrivateRoles);

            isPrivate = hasPermission
                    ? null
                    : false;
        }

        if (isPrivate.HasValue &&
            isPrivate.Value)
        {
            accountLogged.AssertPermission(PermissionKey.GetAllPrivateRoles);
        }

        if (Guard.IsNullOrEmpty(appId) &&
            !accountLogged.HasPermission(PermissionKey.GetAllRolesOfAppsOfAccountLogged) &&
            !accountLogged.HasPermission(PermissionKey.GetAllRolesOfTenant) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            appId = accountLogged.AppLogged.Id;
        }

        if (Guard.IsNotNullOrEmpty(appId))
        {
            accountLogged.AssertPermission(PermissionKey.GetAllRolesOfAppsOfAccountLogged);
        }

        if(Guard.IsNullOrEmpty(tenantId) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            tenantId = accountLogged.Tenant.Id;
        }

        if (Guard.IsNotNullOrEmpty(tenantId))
        {
            accountLogged.AssertPermission(PermissionKey.FullAccess);
        }

        var roles = await roleRepository
            .GetAllAsync(
            appId,
            isPrivate,
            tenantId,
            accountLogged)
            .ConfigureAwait(false);

        return roles;
    }

    public async Task AssertByNameAsync(string name)
    {
        //var existRoleKey = await roleRepository
        //    .ExistByNameAsync(name)
        //    .ConfigureAwait(false);

        //if (existRoleKey)
        //{
        //    throw new SpecificResourceDuplicatedException<Role>(nameof(Role.Name), name);
        //}
        throw new NotImplementedException();
    }

    public async Task CreateAsync(
        CreateRoleArgs args,
        AccountLogged accountLogged)
    {
        //await AssertByNameAsync(args.Name).ConfigureAwait(false);

        if (args.IsDefault)
        {
            await roleRepository
                .RemoveDefaultAsync()
                .ConfigureAwait(false);
        }

        var permissions = await permissionRepository
            .GetAllByKeysAsync(args.PermissionKeys)
            .ConfigureAwait(false);

        var app = accountLogged.AppLogged;
        if (Guard.IsNotNullOrEmpty(args.AppId))
        {
            app = await appService
                .GetByIdAsync(
                args.AppId!,
                accountLogged)
                .ConfigureAwait(false);
        }

        var role = new Role(
            args.Name,
            args.Description,
            args.IsPublic,
            permissions,
            args.IsDefault,
            app);

        await roleRepository
            .CreateAsync(role)
            .ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(
        List<CreateRoleArgs> args,
        AccountLogged accountLogged)
    {
        //var rolesKeys = args
        //    .Select(p => p.Key)
        //    .Distinct()
        //    .ToList();

        //if (rolesKeys.Count != args.Count)
        //{
        //    var duplicatedKeys = args
        //        .GroupBy(r => r.Key)
        //        .Where(g => g.Count() > 1)
        //        .Select(g => g.Key.ToString())
        //        .ToList();

        //    throw new SpecificResourceDuplicatedException<Role>([nameof(Role.Key)], duplicatedKeys);
        //}

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

        //await AssertByKeysAsync(rolesKeys).ConfigureAwait(false);

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

            throw new SpecificResourceNotFoundException<Permission>(
                [nameof(Permission.Key)],
                missingPermissionsMapped);
        }

        var appsIds = args
            .GroupBy(r => r.AppId)
            .Where(a => Guard.IsNotNull(a.Key))
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

        var roles = args
            .ConvertAll(r =>
            {
                var app = accountLogged.AppLogged;
                if (Guard.IsNotNullOrEmpty(r.AppId))
                {
                    app = appsSaved.First(a => a.Id == r.AppId);
                }

                return new Role(
                r.Name,
                r.Description,
                r.IsPublic,
                permissions,
                r.IsDefault,
                app);
            });

        await roleRepository
            .CreateBulkAsync(roles)
            .ConfigureAwait(false);
    }

    //private async Task AssertByKeysAsync(List<RoleKey> rolesKeyes)
    //{
    //    var rolesSaved = await roleRepository
    //        .GetAllByRolesKeyesAsync(rolesKeyes)
    //        .ConfigureAwait(false);

    //    if (rolesSaved.Count != rolesKeyes.Count)
    //    {
    //        var missedKeyes = rolesKeyes
    //            .Where(pk => !rolesSaved.Exists(p => p.Key == pk))
    //            .ToList();
    //        var simpleMissedKeyes = missedKeyes.ConvertAll(r => r.ToString());

    //        throw new SpecificResourceDuplicatedException<Role>([nameof(Role.Key)], simpleMissedKeyes);
    //    }
    //}

    public async Task<bool> HasPermissionAsync(
        List<string> ids,
        PermissionKey permissionKey)
    {
        var existPermission = await roleRepository
            .HasPermissionAsync(
            ids,
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

    public async Task<Role> GetByIdAsync(string id)
    {
        var role = await roleRepository
            .GetByIdAsync(id)
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
