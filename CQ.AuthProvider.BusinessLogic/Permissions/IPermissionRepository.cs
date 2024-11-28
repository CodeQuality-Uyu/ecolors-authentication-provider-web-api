using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Permissions;

public interface IPermissionRepository
{
    Task<Pagination<Permission>> GetAllAsync(
        Guid? appId,
        bool? isPrivate,
        Guid? roleId,
        int page,
        int pageSize,
        AccountLogged accountLogged);

    Task<List<Permission>> GetAllByKeysAsync(
        List<(Guid appId, List<string> keys)> keys,
        AccountLogged accountLogged);

    Task CreateBulkAndSaveAsync(List<Permission> permissions);
}
