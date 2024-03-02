using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal sealed class RoleMongoService : RoleService<RoleMongo>
    {
        public RoleMongoService(
            IRoleRepository<RoleMongo> roleRepository,
            IPermissionInternalService permissionService) 
            : base(roleRepository, permissionService)
        {
        }

        protected override async Task SaveNewRoleAsync(CreateRole newRole)
        {
            await base._permissionService.CheckExistenceAsync(newRole.PermissionKeys).ConfigureAwait(false);
            
            var role = new RoleMongo(
                newRole.Name,
                newRole.Description,
                newRole.Key,
                newRole.PermissionKeys,
                newRole.IsPublic);

            await base._roleRepository.CreateAsync(role).ConfigureAwait(false);
        }
    }
}
