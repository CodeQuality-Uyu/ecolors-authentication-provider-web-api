namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal interface IRoleInternalService<TRole> : IRoleService
        where TRole : class
    {
        Task ExistByKeyAsync(RoleKey key);

        Task<TRole> GetByKeyAsync(RoleKey key);

        Task<bool> HasPermissionAsync(List<RoleKey> keys, PermissionKey permission);
    }
}
