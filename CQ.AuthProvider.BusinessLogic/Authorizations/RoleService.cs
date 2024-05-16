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

            if (existRoleKey)
                throw new SpecificResourceNotFoundException<Role>(nameof(Role.Key), key.ToString());
        }

        protected abstract Task<bool> ExistByKeyAsync(RoleKey key);
        #endregion

        #region GetAll
        async Task<List<Role>> IRoleService.GetAllAsync(Account accountLogged, bool isPrivate = false)
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

        protected abstract Task<List<Role>> GetAllAsync(Account accountLogged, bool isPrivate = false);
        #endregion

        #region Create
        async Task IRoleService.CreateAsync(CreateRole role)
        {
            await this.AssertByKeyAsync(role.Key).ConfigureAwait(false);

            if (role.IsDefault)
            {
                await this.RemoveDefaultAsync().ConfigureAwait(false);
            }

            await this.CreateAsync(role).ConfigureAwait(false);
        }

        protected abstract Task RemoveDefaultAsync();

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

                throw new SpecificResourceDuplicatedException<Role>(new List<string> { nameof(Role.Key) }, duplicatedKeys);
            }

            var defaultRoles = roles
                .GroupBy(r => r.IsDefault)
                .Count(g => g.Count() > 1);
            if (defaultRoles > 1)
                throw new SpecificResourceDuplicatedException<Role>(nameof(Role.IsDefault), "true");

            await this.AssertByKeysAsync(rolesKeys).ConfigureAwait(false);

            if (defaultRoles == 1)
                await this.RemoveDefaultAsync();

            await this.CreateBulkAsync(roles).ConfigureAwait(false);
        }

        private async Task AssertByKeysAsync(List<RoleKey> roles)
        {
            var roleKeysMapped = this._mapper.Map<List<string>>(roles);

            var rolesSaved = await this.GetAllByRoleKeyAsync(roleKeysMapped).ConfigureAwait(false);

            if (rolesSaved.Count != 0)
            {
                var rolePermission = roleKeysMapped.Where(pk => !rolesSaved.Any(p => p.Key.ToString() == pk)).ToList();

                throw new SpecificResourceDuplicatedException<Role>(new List<string> { nameof(Role.Key) }, rolePermission);
            }
        }

        protected abstract Task<List<Role>> GetAllByRoleKeyAsync(List<string> roles);

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
            var roleMapped = this._mapper.Map<Role>(role);

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

        public abstract Task<TRole> GetByKeyAsync(RoleKey key);

        async Task<Role> IRoleInternalService.GetByKeyAsync(RoleKey key)
        {
            var role = await this.GetByKeyAsync(key).ConfigureAwait(false);

            return this._mapper.Map<Role>(role);
        }

        public abstract Task<Role> GetDefaultAsync();
    }
}
