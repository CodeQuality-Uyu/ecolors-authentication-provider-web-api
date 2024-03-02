using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public interface IRoleRepository<TRole> : IRepository<TRole>
        where TRole : class
    {
        Task<bool> ExistByKeyAsync(RoleKey key);

        Task<RoleInfo> GetInfoByIdAsync<TException>(string id, TException exception)
            where TException : Exception;

        Task AddPermissionsByIdAsync(string id, List<Permission> permissions);

        Task<List<RoleInfo>> GetAllInfoAsync(bool isPublic, AccountInfo accountLogged);

        Task<bool> HasPermissionByKeysAsync(List<RoleKey> keys, PermissionKey permissionKey);
    }
}
