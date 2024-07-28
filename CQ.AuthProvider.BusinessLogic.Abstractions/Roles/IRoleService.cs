using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

public interface IRoleService
{
    Task<List<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? tenantId,
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
    Task AssertByNameAsync(string id);

    Task<bool> HasPermissionAsync(
        List<string> ids,
        PermissionKey permission);

    Task<Role> GetByIdAsync(string id);

    Task<Role> GetDefaultAsync();
}

