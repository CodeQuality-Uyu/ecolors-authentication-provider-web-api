using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

public interface IRoleService
{
    Task<List<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        AccountLogged accountLogged);

    Task CreateAsync(
        CreateRoleArgs role,
        AccountLogged accountLogged);

    Task CreateBulkAsync(
        List<CreateRoleArgs> roles,
        AccountLogged accountLogged);

    Task AddPermissionByIdAsync(
        string id,
        AddPermissionArgs permissions);
}

internal interface IRoleInternalService : IRoleService
{
    Task AssertByKeyAsync(RoleKey key);

    Task<bool> HasPermissionAsync(
        List<RoleKey> keys,
        PermissionKey permission);

    Task<Role> GetByKeyAsync(RoleKey key);

    Task<Role> GetDefaultAsync();
}

