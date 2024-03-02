using AutoMapper;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Authorizations.Mappings;
using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.AuthProvider.DataAccess.Contexts;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.EfCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace CQ.AuthProvider.DataAccess.Roles
{
    internal sealed class RoleEfCoreRepository : EfCoreRepository<RoleEfCore>, IRoleRepository<RoleEfCore>
    {
        private readonly IMapper _mapper;

        private readonly IRepository<RolePermission> _rolePermissionRepository;

        public RoleEfCoreRepository(EfCoreContext efCoreContext)
            : base(efCoreContext)
        {
            var config = new MapperConfiguration(conf => conf.AddProfile<RoleProfile>());
            this._mapper = config.CreateMapper();
            this._rolePermissionRepository = new EfCoreRepository<RolePermission>(efCoreContext);
        }

        public async Task AddPermissionsByIdAsync(string id, List<Permission> permissions)
        {
            var rolesPermissions = permissions.Select(p => new RolePermission(id, p.Id)).ToList();

            await this._rolePermissionRepository.CreateBulkAsync(rolesPermissions).ConfigureAwait(false);
        }

        public async Task<bool> ExistByKeyAsync(RoleKey key)
        {
            return await base.ExistAsync(r => r.Key == key.ToString()).ConfigureAwait(false);
        }

        public async Task<List<RoleInfo>> GetAllInfoAsync(bool isPrivate, AccountInfo accountLogged)
        {
            if (isPrivate)
            {
                var hasPermission = accountLogged.Permissions.Any(p => p == PermissionKey.GetPrivateRoles || p == PermissionKey.Any);
                if (!hasPermission)
                {
                    throw new AccessDeniedException(PermissionKey.GetPrivateRoles);
                }
            }

            var roles = await base.GetAllAsync(r => r.IsPublic != isPrivate).ConfigureAwait(false);

            return this._mapper.Map<List<RoleInfo>>(roles);
        }

        public async Task<bool> HasPermissionByKeysAsync(List<RoleKey> keys, PermissionKey permissionKey)
        {
            var rolesKeys = keys.Select(r => r.ToString()).ToList();

            var role = await ExistAsync(
                r => rolesKeys.Contains(r.Key) && r.Permissions.Any(p => p.Key == permissionKey.ToString()))
                .ConfigureAwait(false);

            return role;
        }

        public async Task<RoleInfo> GetInfoByIdAsync<TException>(string id, TException exception)
            where TException : Exception
        {
            var role = await base.GetByIdAsync(id, exception).ConfigureAwait(false);

            return this._mapper.Map<RoleInfo>(role);
        }
    }
}
