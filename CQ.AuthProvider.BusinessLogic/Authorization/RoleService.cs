using CQ.AuthProvider.BusinessLogic.Exceptions;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic
{
    internal sealed class RoleService : IRoleInternalService
    {
        private readonly IRoleRepository _roleRepository;

        private readonly IPermissionInternalService _permissionService;

        public RoleService(
            IRoleRepository roleRepository,
            IPermissionInternalService permissionService)
        {
            this._roleRepository = roleRepository;
            this._permissionService = permissionService;
        }

        public async Task ExistByKeyAsync(RoleKey key)
        {
            var existRoleKey = await this._roleRepository.ExistAsync(r => r.Key == key.ToString());

            if (!existRoleKey)
            {
                throw new SpecificResourceNotFoundException<Role>(nameof(Role.Key), key.ToString());
            }
        }

        public async Task<Role> GetByKeyAsync(RoleKey key)
        {
            var role = await this._roleRepository.GetByPropAsync(key.ToString(), nameof(Role.Key)).ConfigureAwait(false);

            return role;
        }

        public async Task<IList<MiniRole>> GetAllPublicAsync()
        {
            return await this._roleRepository.GetAllAsync<MiniRole>(r => r.IsPublic).ConfigureAwait(false);
        }

        public async Task<IList<Role>> GetAllAsync()
        {
            return await this._roleRepository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task CreateAsync(CreateRole role)
        {
            var existKey = await this._roleRepository.ExistAsync(r => r.Key == role.Key).ConfigureAwait(false);

            if (existKey)
            {
                throw new ResourceDuplicatedException(nameof(Role.Key), role.Key, nameof(Role));
            }

            var permissions = await this._permissionService.GetAllByKeysAsync(role.PermissionKeys).ConfigureAwait(false);

            var newRole = new Role(role.Name, role.Description, role.Key, permissions, role.IsPublic);

            await this._roleRepository.CreateAsync(newRole).ConfigureAwait(false);
        }

        public async Task<bool> HasPermissionAsync(List<RoleKey> roles, string permission)
        {
            var rolesKeys = roles.Select(r => r.ToString()).ToList();

            var role = await this._roleRepository.ExistAsync(
                r => rolesKeys.Contains(r.Key) && r.Permissions.Any(p => p.Key == permission))
                .ConfigureAwait(false);

            return role;
        }

        public async Task AddPermissionByIdAsync(string id, AddPermission permissions)
        {
            var role = await this._roleRepository.GetByIdAsync(id, new SpecificResourceNotFoundException<Role>(nameof(Role.Id), id)).ConfigureAwait(false);

            var permissionsSaved = await this._permissionService.GetAllByKeysAsync(permissions.PermissionsKeys).ConfigureAwait(false);

            var duplicatePermission = permissions.PermissionsKeys.Where(p => role.Permissions.Any(ps => ps.Key == p));
            if (duplicatePermission.Any()) throw new PermissionsDuplicatedException(duplicatePermission.ToList());

            await this._roleRepository.AddPermissionsByIdAsync(id, permissionsSaved).ConfigureAwait(false);
        }
    }
}
