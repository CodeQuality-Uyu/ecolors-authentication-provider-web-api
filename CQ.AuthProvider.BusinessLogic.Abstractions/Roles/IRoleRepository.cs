

using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

internal interface IRoleRepository
{
    Task<List<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        bool viewAll,
        AccountLogged accountLogged);

    Task<bool> ExistByKeyAsync(RoleKey key);

    Task RemoveDefaultAsync();

    Task CreateAsync(Role role);

    Task CreateBulkAsync(List<Role> roles);

    Task<List<Role>> GetAllByRolesKeyesAsync(List<RoleKey> rolesKeys);

    Task<bool> HasPermissionAsync(
        List<RoleKey> rolesKeys,
        PermissionKey permissionKey);

    Task<Role> GetByIdAsync(string id);

    Task<List<PermissionKey>> FilterDuplicatedPermissionsAsync(
        string id,
        List<PermissionKey> permissionKey);

    Task AddPermissionsAsync(
        string id,
        List<PermissionKey> permissionsKeys);

    Task<Role> GetByKeyAsync(RoleKey key);

    Task<Role> GetDefaultAsync();
}
