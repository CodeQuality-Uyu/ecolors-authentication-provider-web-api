using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

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

internal interface IRoleInternalService
    : IRoleService
{
    Task<Role> GetByIdAsync(string id);

    Task<Role> GetDefaultAsync();
}

