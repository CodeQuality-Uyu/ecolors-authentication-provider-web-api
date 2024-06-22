using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using System.Security;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

public interface IRoleService
{
    Task<List<Role>> GetAllAsync(
        bool isPrivate,
        Account accountLogged);

    Task CreateAsync(CreateRoleArgs role);

    Task CreateBulkAsync(List<CreateRoleArgs> roles);

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

internal interface IRoleInternalService<TRole> : IRoleInternalService
        where TRole : class
{
    new Task<TRole> GetByKeyAsync(RoleKey key);
}

