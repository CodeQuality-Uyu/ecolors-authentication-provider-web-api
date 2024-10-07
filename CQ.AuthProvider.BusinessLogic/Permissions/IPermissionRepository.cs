using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Permissions;

public interface IPermissionRepository
{
    Task<Pagination<Permission>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? roleId,
        int page,
        int pageSize,
        AccountLogged accountLogged);

    Task<List<Permission>> GetAllByKeysAsync(
        List<(string appId, List<string> keys)> keys,
        AccountLogged accountLogged);

    Task CreateBulkAndSaveAsync(List<Permission> permissions);
}
