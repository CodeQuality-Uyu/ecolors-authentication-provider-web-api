using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

public interface IPermissionService
{
    Task<List<Permission>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? roleId,
        string? tenantId,
        AccountLogged accountLogged);

    Task CreateAsync(
        CreatePermissionArgs permission,
        AccountLogged accountLogged);

    Task CreateBulkAsync(
        List<CreatePermissionArgs> permission,
        AccountLogged accountLogged);
}

internal interface IPermissionInternalService
    : IPermissionService
{
    Task<List<Permission>> GetExactAllByKeysAsync(
        List<(string appId, List<string> keys)> keys,
        AccountLogged accountLogged);
}