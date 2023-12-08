using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task CheckExistAsync(Roles key)
        {
            var existRoleKey = await this._roleRepository.ExistAsync(r => r.Key == key.ToString());

            if (!existRoleKey)
            {
                throw new SpecificResourceNotFoundException<Role>(nameof(Role.Key), key.ToString());
            }
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

            await this._permissionService.CheckExistenceAsync(role.PermissionKeys).ConfigureAwait(false);

            var newRole = new Role(role.Name, role.Description, role.Key, role.PermissionKeys, role.IsPublic);

            await this._roleRepository.CreateAsync(newRole).ConfigureAwait(false);
        }

        public async Task<bool> HasPermissionAsync(IList<Roles> roles, string permission)
        {
            var role = await this._roleRepository.ExistAsync(
                r => roles.Contains(new Roles(r.Key)) && r.PermissionKeys.Contains(permission))
                .ConfigureAwait(false);

            return role;
        }

        public async Task AddPermissionByIdAsync(string id, AddPermission permissions)
        {
            var role = await this._roleRepository.GetByPropAsync(id, new SpecificResourceNotFoundException<Role>(nameof(Role.Id), id)).ConfigureAwait(false);

            await this._permissionService.CheckExistenceAsync(permissions.PermissionsKeys).ConfigureAwait(false);

            var duplicatePermission = permissions.PermissionsKeys.Where(p => role.PermissionKeys.Contains(p));
            if (duplicatePermission.Any()) throw new PermissionsDuplicatedException(duplicatePermission.ToList());

            await this._roleRepository.AddPermissionsByIdAsync(id, permissions.PermissionsKeys).ConfigureAwait(false);
        }
    }
}
