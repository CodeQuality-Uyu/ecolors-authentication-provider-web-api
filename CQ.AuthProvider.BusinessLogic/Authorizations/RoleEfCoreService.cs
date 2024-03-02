using AutoMapper;
using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal sealed class RoleEfCoreService : RoleService<RoleEfCore>
    {
        private readonly IMapper _mapper;

        public RoleEfCoreService(
            IRoleRepository<RoleEfCore> roleRepository,
            IPermissionInternalService permissionService,
            IMapper mapper) 
            : base(roleRepository, permissionService)
        {
            this._mapper = mapper;
        }

        protected override async Task SaveNewRoleAsync(CreateRole newRole)
        {
            var permissions = await base._permissionService.GetAllByKeysAsync(newRole.PermissionKeys).ConfigureAwait(false);
            
            var permissionsEfCore = this._mapper.Map<List<PermissionEfCore>>(permissions);

            var role = new RoleEfCore(
                newRole.Name,
                newRole.Description,
                newRole.Key,
                permissionsEfCore,
                newRole.IsPublic);

            await base._roleRepository.CreateAsync(role).ConfigureAwait(false);
        }
    }
}
