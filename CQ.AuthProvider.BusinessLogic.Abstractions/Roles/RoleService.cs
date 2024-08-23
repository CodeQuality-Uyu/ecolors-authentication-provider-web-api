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
    IPermissionInternalService permissionService,
    IAppInternalService appService)
    : IRoleInternalService
{
    public async Task<List<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? tenantId,
        AccountLogged accountLogged)
    {
        AssertCanReadPrivateRolesOrSetDefault(
            ref isPrivate,
            accountLogged);

        AssertCanFilterRolesByAppIdOrSetDefault(
            ref appId,
            accountLogged);

        AssertCanFilterRolesByTenantIdOrSetDefault(
            ref tenantId,
            accountLogged);

        var roles = await roleRepository
            .GetAllAsync(
            appId,
            isPrivate,
            tenantId,
            accountLogged)
            .ConfigureAwait(false);

        return roles;
    }

    private static void AssertCanReadPrivateRolesOrSetDefault(
        ref bool? isPrivate,
        AccountLogged accountLogged)
    {
        if (PermissionService.IsPrivate(isPrivate))
        {
            accountLogged.AssertPermission(PermissionKey.CanReadPrivateRoles);
        }

        if (Guard.IsNull(isPrivate))
        {
            var hasPermission = accountLogged.HasPermission(PermissionKey.CanReadPrivateRoles);

            isPrivate = hasPermission
                    ? null
                    : false;
        }
    }

    private static void AssertCanFilterRolesByAppIdOrSetDefault(
        ref string? appId,
        AccountLogged accountLogged)
    {
        if (Guard.IsNotNullOrEmpty(appId) &&
            !accountLogged.AppsIds.Contains(appId!) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            accountLogged.AssertPermission(PermissionKey.CanReadRolesOfTenant);
        }

        if (Guard.IsNullOrEmpty(appId) &&
            !accountLogged.HasPermission(PermissionKey.CanReadRolesOfTenant) &&
            !accountLogged.HasPermission(PermissionKey.FullAccess))
        {
            appId = accountLogged.AppLogged.Id;
        }
    }

    private static void AssertCanFilterRolesByTenantIdOrSetDefault(
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

    public Task AssertByNameAsync(string name)
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
        await CreateBulkAsync(
            [args],
            accountLogged)
            .ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(
        List<CreateRoleArgs> args,
        AccountLogged accountLogged)
    {
        var defaultRoles = args
            .Where(r => r.IsDefault)
            .GroupBy(r => r.AppId ?? accountLogged.AppLogged.Id)
            .ToList();

        var duplicatedDefaultRolesInApp = defaultRoles
            .Where(g => g.Count() > 1)
            .ToList();
        if (duplicatedDefaultRolesInApp.Count > 1)
        {
            throw new ArgumentException("Only one role can be default in one app");
        }

        if (defaultRoles.Count != 0)
        {
            var defaultAppsIds = defaultRoles.ConvertAll(d => d.Key);
            await roleRepository
                .RemoveDefaultsAndSaveAsync(
                defaultAppsIds,
                accountLogged)
                .ConfigureAwait(false);
        }

        var allPermissionsKeyes = args
            .GroupBy(a => a.AppId ?? accountLogged.AppLogged.Id)
            .Select(g =>
            (g.Key,
            g
            .SelectMany(a => a.PermissionKeys)
            .Distinct()
            .ToList()))
            .ToList();

        var permissions = await permissionService
            .GetExactAllByKeysAsync(
            allPermissionsKeyes,
            accountLogged)
            .ConfigureAwait(false);

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
