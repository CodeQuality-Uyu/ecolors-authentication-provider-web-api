using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

public interface IPermissionService
{
    Task<List<Permission>> GetAllAsync(
        bool? isPrivate,
        string? roleId,
        AccountLogged accountLogged);

    Task CreateAsync(
        CreatePermissionArgs permission,
        AccountLogged accountLogged);

    Task CreateBulkAsync(
        List<CreatePermissionArgs> permission,
        AccountLogged accountLogged);
}

internal interface IPermissionInternalService: IPermissionService
{
    Task AssertByKeysAsync(List<PermissionKey> permissionKeys);

    Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> keys);
}