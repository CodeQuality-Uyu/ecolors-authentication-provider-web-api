using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations.Exceptions;
using CQ.Exceptions;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal abstract class RoleService<TRole>: IRoleInternalService<TRole>
        where TRole : class
    {
        protected readonly IRoleRepository<TRole> _roleRepository;

        protected readonly IPermissionInternalService _permissionService;

        public RoleService(
            IRoleRepository<TRole> roleRepository,
            IPermissionInternalService permissionService)
        {
            this._roleRepository = roleRepository;
            this._permissionService = permissionService;
        }

        public async Task ExistByKeyAsync(RoleKey key)
        {
            var existRoleKey = await this._roleRepository.ExistByKeyAsync(key).ConfigureAwait(false);

            if (!existRoleKey)
            {
                throw new SpecificResourceNotFoundException<RoleEfCore>(nameof(RoleEfCore.Key), key.ToString());
            }
        }

        public async Task<TRole> GetByKeyAsync(RoleKey key)
        {
            var role = await this._roleRepository.GetByPropAsync(key.ToString(), nameof(RoleInfo.Key)).ConfigureAwait(false);

            return role;
        }

        public async Task<List<RoleInfo>> GetAllAsync(bool isPrivate, AccountInfo accountLogged)
        {
            return await this._roleRepository.GetAllInfoAsync(isPrivate, accountLogged).ConfigureAwait(false);
        }

        public async Task CreateAsync(CreateRole role)
        {
            var existKey = await this._roleRepository.ExistByKeyAsync(role.Key).ConfigureAwait(false);

            if (existKey)
            {
                throw new SpecificResourceDuplicatedException<RoleInfo>(nameof(RoleInfo.Key), role.Key.ToString());
            }

            await this.SaveNewRoleAsync(role).ConfigureAwait(false);
        }

        protected abstract Task SaveNewRoleAsync(CreateRole newRole);

        public async Task<bool> HasPermissionAsync(List<RoleKey> roles, PermissionKey permissionKey)
        {
            return await this._roleRepository.HasPermissionByKeysAsync(roles, permissionKey).ConfigureAwait(false);
        }

        public async Task AddPermissionByIdAsync(string id, AddPermission permissions)
        {
            var role = await this._roleRepository.GetInfoByIdAsync(id, new SpecificResourceNotFoundException<RoleInfo>(nameof(RoleInfo.Id), id)).ConfigureAwait(false);

            var permissionsSaved = await this._permissionService.GetAllByKeysAsync(permissions.PermissionsKeys).ConfigureAwait(false);

            var duplicatePermission = permissions.PermissionsKeys
                .Where(p => role.Permissions.Contains(p))
                .ToList();

            if (duplicatePermission.Count != 0)
                throw new PermissionsDuplicatedException(duplicatePermission);

            await this._roleRepository.AddPermissionsByIdAsync(id, permissionsSaved).ConfigureAwait(false);
        }
    }
}
