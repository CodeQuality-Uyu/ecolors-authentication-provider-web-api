using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations.Exceptions;
using CQ.Exceptions;
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

        public async Task<List<Permission>> GetAllAsync(bool isPrivate, string? roleId, AccountInfo accountLogged)
        {
            return await this._permissionRepository.GetAllInfoAsync(isPrivate, roleId, accountLogged).ConfigureAwait(false);
        }

        public async Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> permissionKeys)
        {
            var permissionsSaved = await this._permissionRepository.GetAllByKeysAsync(permissionKeys).ConfigureAwait(false);

            if (permissionsSaved.Count != permissionKeys.Count)
            {
                var permissionsNotFound = permissionKeys.Where(p => !permissionsSaved.Any(ps => new PermissionKey(ps.Key) == p)).ToList();

                throw new PermissionNotFoundException(permissionsNotFound);
            }

            return permissionsSaved;
        }

        public async Task ExistByKeysAsync(List<PermissionKey> permissionKeys)
        {
            await this.GetAllByKeysAsync(permissionKeys).ConfigureAwait(false);
        }

        public async Task CreateAsync(CreatePermission permission)
        {
            var existPermission = await this._permissionRepository.ExistByKeyAsync(permission.Key).ConfigureAwait(false);

            if (existPermission) 
                throw new SpecificResourceDuplicatedException<Permission>(
                    nameof(Permission.Key),
                    permission.Key.ToString());

            await this.SaveNewPermissionAsync(permission).ConfigureAwait(false);
        }

        protected abstract Task SaveNewPermissionAsync(CreatePermission newPermission);
    }
}
