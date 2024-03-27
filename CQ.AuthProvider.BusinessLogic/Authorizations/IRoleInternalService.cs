namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal interface IRoleInternalService<TRole>: IRoleInternalService
        where TRole : class
    {
        new Task<TRole> GetByKeyAsync(RoleKey key);
    }

    internal interface IRoleInternalService : IRoleService
    {
        Task AssertByKeyAsync(RoleKey key);
     
        Task<bool> HasPermissionAsync(List<RoleKey> keys, PermissionKey permission);

        Task<Role> GetByKeyAsync(RoleKey key);

        Task<Role> GetDefaultAsync();
    }
}
