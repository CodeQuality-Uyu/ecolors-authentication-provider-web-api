using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.UnitOfWork.MongoDriver.Core;

namespace CQ.AuthProvider.DataAccess.Mongo.Roles;

internal sealed class RoleRepository(
    AuthDbContext context,
    IMapper mapper)
    : MongoDriverRepository<RoleMongo>(context),
    IRoleRepository
{
    public Task AddPermissionsAsync(string id, List<PermissionKey> permissionsKeys)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Role role)
    {
        throw new NotImplementedException();
    }

    public Task CreateBulkAsync(List<Role> roles)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistByKeyAsync(RoleKey key)
    {
        throw new NotImplementedException();
    }

    public Task<List<PermissionKey>> FilterDuplicatedPermissionsAsync(string id, List<PermissionKey> permissionKey)
    {
        throw new NotImplementedException();
    }

    public Task<List<Role>> GetAllAsync(string? appId, bool? isPrivate, bool viewAll, AccountLogged accountLogged)
    {
        throw new NotImplementedException();
    }

    public Task<List<Role>> GetAllByRolesKeyesAsync(List<RoleKey> rolesKeys)
    {
        throw new NotImplementedException();
    }

    public Task<Role> GetByKeyAsync(RoleKey key)
    {
        throw new NotImplementedException();
    }

    public Task<Role> GetDefaultAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasPermissionAsync(List<RoleKey> rolesKeys, PermissionKey permissionKey)
    {
        throw new NotImplementedException();
    }

    public Task RemoveDefaultAsync()
    {
        throw new NotImplementedException();
    }

    Task<Role> IRoleRepository.GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}
