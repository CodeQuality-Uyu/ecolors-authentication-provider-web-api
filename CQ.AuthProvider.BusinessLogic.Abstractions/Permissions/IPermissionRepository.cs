using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Permissions;

public interface IPermissionRepository
{
    Task<List<Permission>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? roleId,
        string? tenantId,
        AccountLogged accountLogged);

    Task<List<Permission>> GetAllByKeysAsync(
        List<(string appId, List<string> keys)> keys,
        AccountLogged accountLogged);

    Task CreateBulkAsync(List<Permission> permissions);
}
