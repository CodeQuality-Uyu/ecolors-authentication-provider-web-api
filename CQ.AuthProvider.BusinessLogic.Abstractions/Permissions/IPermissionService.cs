using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Permissions;

public interface IPermissionService
{
    Task<Pagination<Permission>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? roleId,
        int page,
        int pageSize,
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