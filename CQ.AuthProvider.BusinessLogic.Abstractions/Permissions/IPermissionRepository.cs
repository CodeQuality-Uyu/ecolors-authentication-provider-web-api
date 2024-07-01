
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

internal interface IPermissionRepository
{
    Task<List<Permission>> GetAllAsync(
        bool? isPrivate,
        string? roleId,
        AccountLogged accountLogged);

    Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> permissionsKeys);

    Task<bool> ExistByKeyAsync(PermissionKey permissionKey);

    Task CreateAsync(Permission permission);

    Task CreateBulkAsync(List<Permission> permissions);
}
