namespace CQ.AuthProvider.BusinessLogic
{
    internal interface IRoleInternalService : IRoleService
    {
        Task ExistByKeyAsync(RoleKey key);

        Task<Role> GetByKeyAsync(RoleKey key);

        Task<bool> HasPermissionAsync(List<RoleKey> keys, string permission);
    }
}
