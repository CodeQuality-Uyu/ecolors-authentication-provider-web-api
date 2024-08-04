
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

internal interface IPermissionRepository
{
    Task<List<Permission>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? roleId,
        string? tenantId,
        AccountLogged accountLogged);

    Task<List<Permission>> GetAllByKeysAsync(
        List<(string key, string appId)> keys,
        AccountLogged accountLogged);

    Task<bool> ExistByKeyAsync(PermissionKey permissionKey);

    Task CreateAsync(Permission permission);

    Task CreateBulkAsync(List<Permission> permissions);
}
