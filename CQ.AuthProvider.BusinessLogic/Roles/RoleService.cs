using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.Abstractions.Repositories;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Roles;

internal sealed class RoleService(
    IRoleRepository roleRepository,
    IPermissionRepository permissionRepository,
    IAppRepository appRepository,
    IUnitOfWork unitOfWork)
    : IRoleInternalService
{
    public async Task<Pagination<Role>> GetAllAsync(
        Guid? appId,
        bool? isPrivate,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var accountNotInApp = !accountLogged.AppsIds.Contains(appId ?? Guid.Empty);
        if (accountNotInApp)
        {
            appId = accountLogged.AppLogged.Id;
        }

        var roles = await roleRepository
            .GetAllAsync(
            appId!.Value,
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
        await AssertAsync(args)
            .ConfigureAwait(false);
            
        var defaultRoles = args
            .Roles
            .Where(r => r.IsDefault)
            .ToList()
            .ConvertAll(r => r.AppId);

        if (defaultRoles.Count != 0)
        {
            await roleRepository
                .RemoveDefaultsAsync(
                defaultRoles,
                accountLogged)
                .ConfigureAwait(false);
        }

        var allPermissionsKeyes = args
            .Roles
            .SelectMany(r => r.PermissionKeys.ConvertAll(p => (r.AppId, p)).Distinct())
            .ToList();

        var permissions = await permissionRepository
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
                var app = accountLogged.Apps.FirstOrDefault(a => a.Id == r.AppId);
                var permissionsOfRole = permissions
                    .Where(p => p.App.Id == r.AppId && r.PermissionKeys.Contains(p.Key))
                    .ToList();

                return new Role
                {
                    Name = r.Name,
                    Description = r.Description,
                    IsPublic = r.IsPublic,
                    Permissions = permissionsOfRole,
                    IsDefault = r.IsDefault,
                    App = app,
                    Tenant = accountLogged.Tenant
                };
            });

        await roleRepository
            .CreateBulkAsync(roles)
            .ConfigureAwait(false);

        await unitOfWork
            .CommitChangesAsync()
            .ConfigureAwait(false);
    }

    private async Task AssertAsync(
        CreateBulkRoleArgs args)
    {
        var roles = args
        .Roles
        .Select(r => (r.AppId, r.Name))
        .ToList();

        var rolesCreated = await roleRepository
            .GetAllByAppAndNamesAsync(roles)
            .ConfigureAwait(false);

        if (rolesCreated.Count != 0)
        {
            throw new InvalidOperationException($"The following roles already exist: {string.Join(",", rolesCreated)}");
        }
    }

    public async Task AddPermissionByIdAsync(
        Guid id,
        AddPermissionArgs args)
    {
        await AssertAsync(
            id,
            args)
            .ConfigureAwait(false);

        await roleRepository
            .AddPermissionsAsync(
            id,
            args.PermissionIds)
            .ConfigureAwait(false);
    }

    private async Task AssertAsync(
        Guid id,
        AddPermissionArgs args)
    {
        var role = await roleRepository
            .GetByIdAsync(id)
            .ConfigureAwait(false);

        var duplicatedPermissions = role
            .Permissions
            .Where(p => args.PermissionIds.Contains(p.Id))
            .Select(p => p.Key)
            .ToList();
        if (duplicatedPermissions.Count != 0)
        {
            throw new InvalidOperationException($"The following permissions are duplicated in the role: {string.Join(",", duplicatedPermissions)}");
        }

        var permissions = await permissionRepository
            .GetAllAsync(args.PermissionIds)
            .ConfigureAwait(false);
        if (permissions.Count != args.PermissionIds.Count)
        {
            var foundIds = permissions.Select(p => p.Id).ToHashSet();
            var notFoundIds = args.PermissionIds.Where(pid => !foundIds.Contains(pid)).ToList();
            throw new InvalidOperationException($"The following permissions do not exist: {string.Join(",", notFoundIds)}");
        }
    }

    public async Task RemovePermissionByIdAsync(
        Guid id,
        Guid permissionId)
    {
        await roleRepository
            .RemovePermissionByIdAsync(
            id,
            permissionId)
            .ConfigureAwait(false);
    }
}
