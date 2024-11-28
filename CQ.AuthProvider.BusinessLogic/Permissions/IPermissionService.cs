using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Permissions;

public interface IPermissionService
{
    Task<Pagination<Permission>> GetAllAsync(
        Guid? appId,
        bool? isPrivate,
        Guid? roleId,
        int page,
        int pageSize,
        AccountLogged accountLogged);

    Task CreateAsync(
        CreatePermissionArgs args,
        AccountLogged accountLogged);

    Task CreateBulkAsync(
        CreateBulkPermissionArgs args,
        AccountLogged accountLogged);
}

internal interface IPermissionInternalService
    : IPermissionService
{
    Task<List<Permission>> GetExactAllByKeysAsync(
        List<(Guid appId, List<string> keys)> keys,
        AccountLogged accountLogged);
}