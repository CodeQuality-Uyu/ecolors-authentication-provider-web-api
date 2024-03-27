using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal sealed class RoleEfCoreService : RoleService<RoleEfCore>
    {
        private readonly IRepository<RoleEfCore> _roleRepository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly IPermissionInternalService<PermissionEfCore> _permissionService;

        public RoleEfCoreService(
            IRepository<RoleEfCore> roleRepository,
            IRepository<RolePermission> _rolePermissionRepository,
            IPermissionInternalService<PermissionEfCore> permissionService,
            IMapper mapper) 
            : base(mapper)
        {
            this._roleRepository = roleRepository;
            this._rolePermissionRepository = _rolePermissionRepository;
            this._permissionService = permissionService;
        }

        protected override async Task<bool> HasPermissionAsync(List<string> roles, string permissionKey)
        {
            var existPermission = await this._roleRepository
                .ExistAsync(
                r => roles.Contains(r.Key) && r.Permissions.Any(p => p.Key == permissionKey))
                .ConfigureAwait(false);

            return existPermission;
        }

        protected override async Task CreateAsync(CreateRole newRole)
        {
            var permissions = await this._permissionService
                .GetAllByKeysAsync(newRole.PermissionKeys)
                .ConfigureAwait(false);

            var role = new RoleEfCore(
                newRole.Name,
                newRole.Description,
                newRole.Key,
                permissions,
                newRole.IsPublic,
                newRole.IsDefault);

            await this._roleRepository.CreateAsync(role).ConfigureAwait(false);
        }

        protected override async Task RemoveDefaultAsync()
        {
            var roleDefault = await this._roleRepository.GetOrDefaultAsync(r => r.IsDefault).ConfigureAwait(false);

            if (Guard.IsNull(roleDefault))
                return;

            roleDefault.IsDefault = false;
            await this._roleRepository.UpdateAsync(roleDefault).ConfigureAwait(false);
        }

        protected override async Task<bool> ExistByKeyAsync(RoleKey key)
        {
            var roleValue = key.ToString();

            var existRole = await this._roleRepository.ExistAsync(r => r.Key == roleValue).ConfigureAwait(false);

            return existRole;
        }

        protected override async Task<List<Role>> GetAllAsync(Account accountLogged, bool isPrivate = false)
        {
            var roles = await this._roleRepository.GetAllAsync(r => r.IsPublic != isPrivate).ConfigureAwait(false);

            return base._mapper.Map<List<Role>>(roles);
        }

        #region AddPermission
        protected override async Task AddPermissionsByIdAsync(RoleEfCore role, List<PermissionKey> permissions)
        {
            var permissionsSaved = await this._permissionService.GetAllByKeysAsync(permissions).ConfigureAwait(false);

            var rolesPermissions = permissionsSaved.Select(p => new RolePermission(role.Id, p.Id)).ToList();

            await this._rolePermissionRepository.CreateBulkAsync(rolesPermissions).ConfigureAwait(false);
        }

        protected override async Task<RoleEfCore> GetByIdAsync(string id)
        {
            var role = await this._roleRepository.GetByIdAsync(id).ConfigureAwait(false);

            return role;
        }
        #endregion

        public override async Task<RoleEfCore> GetByKeyAsync(RoleKey key)
        {
            var role = await this._roleRepository.GetByPropAsync(key.ToString(), nameof(RoleEfCore.Key)).ConfigureAwait(false);

            return role;
        }

        public override async Task<Role> GetDefaultAsync()
        {
            var role = await this._roleRepository.GetAsync(r => r.IsDefault).ConfigureAwait(false);

            return base._mapper.Map<Role>(role);
        }

        #region CreateBulk
        protected override async Task<List<Role>> GetAllByRoleKeyAsync(List<string> roles)
        {
            var rolesSaved = await this._roleRepository.GetAllAsync(r => roles.Contains(r.Key)).ConfigureAwait(false);

            return base._mapper.Map<List<Role>>(rolesSaved);
        }

        protected override async Task CreateBulkAsync(List<CreateRole> roles)
        {
            var permissionKeys = roles
                .SelectMany(r => r.PermissionKeys)
                .ToList();

            var permissionsSaved = await this
                ._permissionService
                .GetAllByKeysAsync(permissionKeys)
                .ConfigureAwait(false);

            var rolesToSave = roles
                .Select(r =>
                {
                    var permissions = permissionsSaved.Where(p => r.PermissionKeys.Contains(new PermissionKey(p.Key))).ToList();

                    return new RoleEfCore(
                        r.Name,
                        r.Description,
                        r.Key,
                        permissions,
                        r.IsPublic);
                })
                .ToList();

            await this._roleRepository.CreateBulkAsync(rolesToSave).ConfigureAwait(false);
        }
        #endregion
    }
}
