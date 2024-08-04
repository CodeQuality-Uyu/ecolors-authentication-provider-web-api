

using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

internal interface IRoleRepository
{
    Task<List<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? tenantId,
        AccountLogged accountLogged);

    Task RemoveDefaultAsync();

    Task CreateAsync(Role role);

    Task CreateBulkAsync(List<Role> roles);

    Task<bool> HasPermissionAsync(
        List<string> ids,
        PermissionKey permissionKey);

    Task<Role> GetByIdAsync(string id);

    Task<List<PermissionKey>> FilterDuplicatedPermissionsAsync(
        string id,
        List<string> permissionKey);

    Task AddPermissionsAsync(
        string id,
        List<string> permissionsKeys);

    Task<Role> GetDefaultAsync();
}
