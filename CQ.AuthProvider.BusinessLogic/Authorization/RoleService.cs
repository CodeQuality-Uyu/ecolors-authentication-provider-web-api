using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    internal sealed class RoleService : IRoleInternalService
    {
        private readonly IRepository<Role> _roleRepository;

        private readonly IPermissionInternalService _permissionService;

        public RoleService(
            IRepository<Role> roleRepository,
            IPermissionInternalService permissionService)
        {
            this._roleRepository = roleRepository;
            this._permissionService = permissionService;
        }

        public async Task CheckExistAsync(Roles key)
        {
            var existRoleKey = await this._roleRepository.ExistAsync(r => r.Key == key.Value);

            if (!existRoleKey)
            {
                throw new SpecificResourceNotFoundException<Role>(nameof(Role.Key), key.ToString());
            }
        }

        public async Task<IList<MiniRole>> GetAllAsync()
        {
            return await this._roleRepository.GetAllAsync<MiniRole>(r => r.IsPublic).ConfigureAwait(false);
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

        public async Task<Role> GetByKeyAsync(Roles key)
        {
            var role = await this._roleRepository.GetByPropAsync(
                key.Value,
                new SpecificResourceNotFoundException<Role>(nameof(Role.Key), key.Value),
                nameof(Role.Key))
                .ConfigureAwait(false);

            return role;
        }
    }
}
