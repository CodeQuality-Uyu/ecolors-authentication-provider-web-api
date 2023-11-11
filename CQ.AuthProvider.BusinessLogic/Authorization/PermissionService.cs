using CQ.AuthProvider.BusinessLogic.Authorization.Exceptions;
using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.UnitOfWork.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    internal sealed class PermissionService : IPermissionInternalService
    {
        private readonly IRepository<Permission> _permissionRepository;

        public PermissionService(IRepository<Permission> permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IList<MiniPermission>> GetAllAsync()
        {
            return await this._permissionRepository.GetAllAsync<MiniPermission>(p => p.IsPublic).ConfigureAwait(false);
        }

        public async Task CheckExistenceAsync(IList<string> permissionKeys)
        {
            var permissionsSaved = await this._permissionRepository.GetAllAsync(p => permissionKeys.Contains(p.Key)).ConfigureAwait(false);

            if (permissionsSaved.Count != permissionKeys.Count)
            {
                var permissionsNotFound = permissionKeys.Where(p => !permissionsSaved.Any(ps => ps.Key == p)).ToList();

                throw new PermissionNotFoundException(permissionsNotFound);
            }
        }

        public async Task CreateAsync(CreatePermission permission)
        {
            var existPermission = await this._permissionRepository.ExistAsync(p => p.Key == permission.Key).ConfigureAwait(false);

            if (existPermission)
            {
                throw new ResourceDuplicatedException(nameof(Permission.Key), permission.Key, nameof(Permission));
            }

            var newPermission = new Permission(permission.Name, permission.Description, permission.Key, permission.IsPublic);

            await this._permissionRepository.CreateAsync(newPermission).ConfigureAwait(false);
        }
    }
}
