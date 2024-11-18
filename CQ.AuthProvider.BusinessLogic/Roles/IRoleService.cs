using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Roles;

public interface IRoleService
{
    Task<Pagination<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        int page,
        int pageSize,
        AccountLogged accountLogged);

    Task CreateAsync(
        CreateRoleArgs args,
        AccountLogged accountLogged);

    Task CreateBulkAsync(
        CreateBulkRoleArgs args,
        AccountLogged accountLogged);

    Task AddPermissionByIdAsync(
        string id,
        AddPermissionArgs permissions);
}

internal interface IRoleInternalService
    : IRoleService
{
    Task<Role> GetDefaultAsync();
}

