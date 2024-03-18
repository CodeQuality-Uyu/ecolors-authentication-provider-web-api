using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations.Exceptions;
using CQ.Exceptions;
using System.Data;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal abstract class RoleService<TRole> : IRoleInternalService<TRole>
        where TRole : class
    {
        protected readonly IMapper _mapper;

        public RoleService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        #region ExistByKey
        public async Task AssertByKeyAsync(RoleKey key)
        {
            var existRoleKey = await this.ExistByKeyAsync(key).ConfigureAwait(false);

            if (!existRoleKey)
                throw new SpecificResourceNotFoundException<RoleInfo>(nameof(RoleInfo.Key), key.ToString());
        }

        protected abstract Task<bool> ExistByKeyAsync(RoleKey key);
        #endregion

        public abstract Task<TRole> GetByKeyAsync(RoleKey key);

        #region GetAll
        async Task<List<RoleInfo>> IRoleService.GetAllAsync(AccountInfo accountLogged, bool isPrivate = false)
        {
            if (isPrivate)
            {
                var hasPermission = accountLogged.Permissions.Any(p => p == PermissionKey.GetAllPrivateRoles || p == PermissionKey.Joker);
                if (!hasPermission)
                    throw new AccessDeniedException(PermissionKey.GetAllPrivateRoles.ToString());
            }

            var permissions = await this.GetAllAsync(accountLogged, isPrivate).ConfigureAwait(false);

            return permissions;
        }

        protected abstract Task<List<RoleInfo>> GetAllAsync(AccountInfo accountLogged, bool isPrivate = false);
        #endregion

        #region Create
        async Task IRoleService.CreateAsync(CreateRole role)
        {
            await this.AssertByKeyAsync(role.Key).ConfigureAwait(false);

            await this.CreateAsync(role).ConfigureAwait(false);
        }

        protected abstract Task CreateAsync(CreateRole newRole);
        #endregion

        #region CreateBulk
        async Task IRoleService.CreateBulkAsync(List<CreateRole> roles)
        {
            var rolesKeys = roles
                .Select(p => p.Key)
                .Distinct()
                .ToList();

            if (rolesKeys.Count != roles.Count)
            {
                var duplicatedKeys = roles
                    .GroupBy(r => r.Key)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key.ToString())
                    .ToList();

                throw new SpecificResourceDuplicatedException<RoleInfo>(new List<string> { nameof(RoleInfo.Key) }, duplicatedKeys);
            }

            await this.AssertByKeysAsync(rolesKeys).ConfigureAwait(false);

            await this.CreateBulkAsync(roles).ConfigureAwait(false);
        }

        private async Task AssertByKeysAsync(List<RoleKey> roles)
        {
            var roleKeysMapped = this._mapper.Map<List<string>>(roles);

            var rolesSaved = await this.GetAllByRoleKeyAsync(roleKeysMapped).ConfigureAwait(false);

            if (rolesSaved.Count == roles.Count)
                return;

            var rolePermission = roleKeysMapped.Where(pk => !rolesSaved.Any(p => p.Key.ToString() == pk)).ToList();

            throw new SpecificResourceNotFoundException<RoleInfo>(new List<string> { nameof(RoleInfo.Key) }, rolePermission);
        }

        protected abstract Task<List<RoleInfo>> GetAllByRoleKeyAsync(List<string> roles);

        protected abstract Task CreateBulkAsync(List<CreateRole> roles);
        #endregion

        #region HasPermission
        public async Task<bool> HasPermissionAsync(List<RoleKey> roles, PermissionKey permissionKey)
        {
            var rolesMapped = this._mapper.Map<List<string>>(roles);

            var existPermission = await this.HasPermissionAsync(rolesMapped, permissionKey.ToString()).ConfigureAwait(false);

            return existPermission;
        }

        protected abstract Task<bool> HasPermissionAsync(List<string> roles, string permissionKey);
        #endregion

        #region AddPermissionById
        public async Task AddPermissionByIdAsync(string id, AddPermission permissions)
        {
            var role = await this.GetByIdAsync(id).ConfigureAwait(false);
            var roleMapped = this._mapper.Map<RoleInfo>(role);

            var duplicatePermission = permissions.PermissionsKeys
                .Where(p => roleMapped.Permissions.Contains(p))
                .ToList();

            if (duplicatePermission.Count != 0)
                throw new PermissionsDuplicatedException(duplicatePermission);

            await this.AddPermissionsByIdAsync(role, permissions.PermissionsKeys).ConfigureAwait(false);
        }

        protected abstract Task<TRole> GetByIdAsync(string id);

        protected abstract Task AddPermissionsByIdAsync(TRole role, List<PermissionKey> permissions);

        #endregion
    }
}
