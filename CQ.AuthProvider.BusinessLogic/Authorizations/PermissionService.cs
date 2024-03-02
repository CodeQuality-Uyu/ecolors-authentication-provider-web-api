using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Authorizations.Exceptions;
using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal abstract class PermissionService<TPermission> : IPermissionInternalService
        where TPermission : class
    {
        protected readonly IPermissionRepository<TPermission> _permissionRepository;

        public PermissionService(IPermissionRepository<TPermission> permissionRepository)
        {
            this._permissionRepository = permissionRepository;
        }

        public async Task<List<Permission>> GetAllAsync()
        {
            return await this._permissionRepository.GetAllInfoAsync().ConfigureAwait(false);
        }

        public async Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> permissionKeys)
        {
            return await this._permissionRepository.GetAllByKeysAsync(permissionKeys).ConfigureAwait(false);
        }

        public async Task CheckExistenceAsync(List<PermissionKey> permissionKeys)
        {
            var permissionsSaved = await this._permissionRepository.GetAllByKeysAsync(permissionKeys).ConfigureAwait(false);

            if (permissionsSaved.Count != permissionKeys.Count)
            {
                var permissionsNotFound = permissionKeys.Where(p => !permissionsSaved.Any(ps => new PermissionKey(ps.Key) == p)).ToList();

                throw new PermissionNotFoundException(permissionsNotFound);
            }
        }

        public async Task CreateAsync(CreatePermission permission)
        {
            var existPermission = await this._permissionRepository.ExistByKeyAsync(permission.Key).ConfigureAwait(false);

            if (existPermission) 
                throw new ResourceDuplicatedException(
                    nameof(Permission.Key),
                    permission.Key.ToString(),
                    nameof(Permission));

            await this.SaveNewPermissionAsync(permission).ConfigureAwait(false);
        }

        protected abstract Task SaveNewPermissionAsync(CreatePermission newPermission);
    }
}
