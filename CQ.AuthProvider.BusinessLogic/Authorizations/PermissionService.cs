using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Exceptions;
using CQ.Utility;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal abstract class PermissionService<TPermission> : IPermissionInternalService<TPermission>
        where TPermission : class
    {
        protected readonly IMapper _mapper;

        public PermissionService(
            IMapper mapper)
        {
            this._mapper = mapper;
        }

        #region GetAll
        async Task<List<Permission>> IPermissionService.GetAllAsync(Account accountLogged, bool isPrivate = false, string? roleId = null)
        {
            if (isPrivate)
                accountLogged.AssertPermission(PermissionKey.GetAllPrivatePermissions);

            if (Guard.IsNotNullOrEmpty(roleId))
                accountLogged.AssertPermission(PermissionKey.GetAllPermissionsByRoleId);

            var permissions = await this.GetAllAsync(accountLogged, isPrivate, roleId).ConfigureAwait(false);

            return permissions;
        }

        protected abstract Task<List<Permission>> GetAllAsync(Account accountLogged, bool isPrivate = false, string? roleId = null);
        #endregion

        public async Task<List<TPermission>> GetAllByKeysAsync(List<PermissionKey> permissionKeys)
        {
            var permissionKeysMapped = this._mapper.Map<List<string>>(permissionKeys);

            var permissionsSaved = await this.GetAllByPermissionKeyAsync(permissionKeysMapped).ConfigureAwait(false);

            if (permissionsSaved.Count == permissionKeys.Count)
                return permissionsSaved;

            var permissionsMapped = this._mapper.Map<List<Permission>>(permissionsSaved);
            var missingPermissions = permissionKeysMapped.Where(pk => !permissionsMapped.Any(p => p.Key.ToString() == pk)).ToList();

            throw new SpecificResourceNotFoundException<Permission>(new List<string> { nameof(Permission.Key) }, missingPermissions);
        }

        public async Task AssertByKeysAsync(List<PermissionKey> permissionKeys)
        {
            await this.GetAllByKeysAsync(permissionKeys).ConfigureAwait(false);
        }

        #region Create
        async Task IPermissionService.CreateAsync(CreatePermission permission)
        {
            var existPermission = await this.ExistByKeyAsync(permission.Key).ConfigureAwait(false);

            if (existPermission)
                throw new SpecificResourceDuplicatedException<PermissionMongo>(
                    nameof(PermissionMongo.Key),
                    permission.Key.ToString());

            await this.CreateAsync(permission).ConfigureAwait(false);
        }

        protected abstract Task<bool> ExistByKeyAsync(PermissionKey permissionKey);

        protected abstract Task CreateAsync(CreatePermission newPermission);

        #endregion

        #region CreateBulk
        async Task IPermissionService.CreateBulkAsync(List<CreatePermission> permissions)
        {
            var permissionKeys = permissions
                .Select(p => p.Key)
                .Distinct()
                .ToList();

            if (permissionKeys.Count != permissions.Count)
            {
                var duplicatedKeys = permissions
                    .GroupBy(r => r.Key)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key.ToString())
                    .ToList();

                throw new SpecificResourceDuplicatedException<Permission>(new List<string> { nameof(Permission.Key) }, duplicatedKeys);
            }

            await this.AssertByKeysAsync(permissionKeys).ConfigureAwait(false);

            await this.CreateBulkAsync(permissions).ConfigureAwait(false);
        }

        protected abstract Task<List<TPermission>> GetAllByPermissionKeyAsync(List<string> permissions);

        protected abstract Task CreateBulkAsync(List<CreatePermission> permissions);
        #endregion
    }
}
