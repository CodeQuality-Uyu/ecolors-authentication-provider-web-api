using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.Utility;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Roles;

internal sealed class RoleService(
    IRoleRepository roleRepository,
    IPermissionInternalService permissionService)
    : IRoleInternalService
{
    public async Task<Pagination<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var roles = await roleRepository
            .GetAllAsync(
            appId,
            isPrivate,
            page,
            pageSize,
            accountLogged)
            .ConfigureAwait(false);

        return roles;
    }

    public async Task CreateAsync(
        CreateRoleArgs args,
        AccountLogged accountLogged)
    {
        await CreateBulkAsync(
            new CreateBulkRoleArgs([args]),
            accountLogged)
            .ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(
        CreateBulkRoleArgs args,
        AccountLogged accountLogged)
    {
        var defaultRoles = args
            .Roles
            .Where(r => r.IsDefault)
            .GroupBy(r => r.AppId ?? accountLogged.AppLogged.Id)
            .ToList();

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
            .Roles
            .GroupBy(a => a.AppId ?? accountLogged.AppLogged.Id)
            .Select(g =>
            (g.Key, g
            .SelectMany(a => a.PermissionKeys)
            .Distinct()
            .ToList()))
            .ToList();

        var permissions = await permissionService
            .GetExactAllByKeysAsync(
            allPermissionsKeyes,
            accountLogged)
            .ConfigureAwait(false);

        var roles = args
            .Roles
            .ConvertAll(r =>
            {
                var app = Guard.IsNotNullOrEmpty(r.AppId)
                ? accountLogged.Apps.First(a => a.Id == r.AppId)
                : accountLogged.AppLogged;

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

    public async Task AddPermissionByIdAsync(
        string id,
        AddPermissionArgs args)
    {
        var role = await roleRepository
            .GetByIdAsync(id)
            .ConfigureAwait(false);

        var duplicatedPermissions = role
            .Permissions
            .Where(p => args.PermissionsKeys.Contains(p.Key))
            .Select(p => p.Key)
            .ToList();
        if (duplicatedPermissions.Count != 0)
        {
            throw new InvalidOperationException($"The following permissions are duplicated in the role: {string.Join(",", duplicatedPermissions)}");
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
