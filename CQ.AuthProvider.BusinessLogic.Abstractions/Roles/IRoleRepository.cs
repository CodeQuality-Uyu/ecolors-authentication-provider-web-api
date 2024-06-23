

using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

internal interface IRoleRepository
{
    Task<List<Role>> GetAllAsync(bool? isPrivate);

    Task<bool> ExistByKeyAsync(RoleKey key);

    Task RemoveDefaultAsync();

    Task CreateAsync(Role role);

    Task CreateBulkAsync(List<Role> roles);

    Task<List<Role>> GetAllByRolesKeyesAsync(List<RoleKey> rolesKeyes);

    Task<bool> HasPermissionAsync(
        List<RoleKey> rolesKeyes,
        PermissionKey permissionKey);

    Task<Role> GetByIdAsync(string id);

    Task<List<PermissionKey>> FilterDuplicatedPermissionsAsync(
        string id,
        List<PermissionKey> permissionKey);

    Task AddPermissionsAsync(string id, List<PermissionKey> permissionsKeyes);

    Task<Role> GetByKeyAsync(RoleKey key);

    Task<Role> GetDefaultAsync();
}
