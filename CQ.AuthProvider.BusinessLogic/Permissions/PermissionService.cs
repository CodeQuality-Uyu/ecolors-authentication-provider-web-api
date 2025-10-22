using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Permissions;

internal sealed class PermissionService(IPermissionRepository _permissionRepository)
    : IPermissionInternalService
{
    public async Task<Pagination<Permission>> GetAllAsync(
        Guid? appId,
        bool? isPrivate,
        Guid? roleId,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var permissions = await _permissionRepository
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

    public async Task CreateAsync(
        CreatePermissionArgs args,
        AccountLogged accountLogged)
    {
        await CreateBulkAsync(
            new CreateBulkPermissionArgs([args]),
            accountLogged)
            .ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(
        CreateBulkPermissionArgs args,
        AccountLogged accountLogged)
    {
        var allPermissionsKeys = args
            .Permissions
            .ConvertAll(a => (a.AppId, a.Key))
            .Distinct()
            .ToList();

        var duplicatedPermissions = await _permissionRepository
            .GetAllByKeysAsync(
            allPermissionsKeys,
            accountLogged)
            .ConfigureAwait(false);
        if (duplicatedPermissions.Count != 0)
        {
            var permissionsSavedKeys = duplicatedPermissions
                .ConvertAll(p => (p.App.Id, p.Key));

            throw new InvalidOperationException($"Duplicated keys exist {string.Join(",", permissionsSavedKeys)}");
        }

        var permissions = args
            .Permissions
            .ConvertAll(p =>
        {
            var app = accountLogged.Apps.First(a => a.Id == p.AppId);

            return new Permission(
            p.Name,
            p.Description,
            p.IsPublic,
            p.Key,
            app);
        });

        await _permissionRepository
            .CreateBulkAndSaveAsync(permissions)
            .ConfigureAwait(false);
    }
}
