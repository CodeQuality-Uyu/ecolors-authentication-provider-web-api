using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.UnitOfWork.Abstractions.Repositories;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Roles;

internal sealed class RoleService(
    IRoleRepository _roleRepository,
    IPermissionRepository _permissionRepository)
    : IRoleInternalService
{
    public async Task<Pagination<Role>> GetAllAsync(
        Guid? appId,
        bool? isPrivate,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var roles = await _roleRepository
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
            .ToList()
            .ConvertAll(r => r.AppId ?? accountLogged.AppLogged.Id);

        if (defaultRoles.Count != 0)
        {
            await _roleRepository
                .RemoveDefaultsAndSaveAsync(
                defaultRoles,
                accountLogged)
                .ConfigureAwait(false);
        }

        var allPermissionsKeyes = args
            .Roles
            .SelectMany(r => r.PermissionsKeys.ConvertAll(p => (r.AppId ?? accountLogged.AppLogged.Id, p)).Distinct())
            .ToList();

        var permissions = await _permissionRepository
            .GetAllByKeysAsync(
            allPermissionsKeyes,
            accountLogged)
            .ConfigureAwait(false);
        if (permissions.Count != allPermissionsKeyes.Count)
        {
            var missingPermissions = allPermissionsKeyes
                .Where(pk => !permissions.Exists(p => p.App.Id == pk.Item1 && p.Key == pk.p))
                .ToList();

            throw new InvalidOperationException($"The following permissions do not exist: {string.Join(",", missingPermissions)}");
        }

        var roles = args
            .Roles
            .ConvertAll(r =>
            {
                var app = r.AppId.HasValue
                ? accountLogged.Apps.First(a => a.Id == r.AppId)
                : accountLogged.AppLogged;

                return new Role
                {
                    Name = r.Name,
                    Description = r.Description,
                    IsPublic = r.IsPublic,
                    Permissions = permissions,
                    IsDefault = r.IsDefault,
                    App = app,
                    Tenant = accountLogged.Tenant
                };
            });

        await _roleRepository
            .CreateBulkAsync(roles)
            .ConfigureAwait(false);
    }

    public async Task AddPermissionByIdAsync(
        Guid id,
        AddPermissionArgs args)
    {
        var role = await _roleRepository
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

        await _roleRepository
            .AddPermissionsAsync(
            id,
            args.PermissionsKeys)
            .ConfigureAwait(false);
    }
}
