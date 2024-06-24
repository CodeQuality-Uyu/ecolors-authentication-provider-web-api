using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Exceptions;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.Mongo.Authorizations
{
    internal sealed class PermissionMongoService : PermissionService<PermissionMongo>
    {
        private readonly IRepository<PermissionMongo> _permissionRepository;
        private readonly IRepository<RoleMongo> _roleRepository;

        public PermissionMongoService(
            IRepository<PermissionMongo> permissionRepository,
            IRepository<RoleMongo> roleRepository,
            IMapper mapper)
            : base(mapper)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }

        #region Create
        protected override async Task<bool> ExistByKeyAsync(PermissionKey permissionKey)
        {
            var permissionValue = permissionKey.ToString();

            var existPermission = await _permissionRepository.ExistAsync(p => p.Key == permissionValue).ConfigureAwait(false);

            return existPermission;
        }

        protected override async Task CreateAsync(CreatePermission newPermission)
        {
            var permission = new PermissionMongo(
                newPermission.Name,
                newPermission.Description,
                newPermission.Key,
                newPermission.IsPublic);

            await _permissionRepository.CreateAsync(permission).ConfigureAwait(false);
        }
        #endregion

        #region CreateBulk
        protected override async Task<List<PermissionMongo>> GetAllByPermissionKeyAsync(List<string> permissions)
        {
            var permissionsSaved = await _permissionRepository
                .GetAllAsync(p => permissions.Contains(p.Key))
                .ConfigureAwait(false);

            return permissionsSaved;
        }

        protected override async Task CreateBulkAsync(List<CreatePermission> permissions)
        {
            var permissionsMapped = permissions
                .Select(p => new PermissionMongo(
                p.Name,
                p.Description,
                p.Key,
                p.IsPublic))
                .ToList();

            await _permissionRepository.CreateBulkAsync(permissionsMapped).ConfigureAwait(false);
        }
        #endregion

        protected override async Task<List<Permission>> GetAllAsync(Account accountLogged, bool isPrivate = false, string? roleId = null)
        {
            var permissionsToGet = new List<string>();
            if (Guard.IsNotNullOrEmpty(roleId))
            {
                var role = await _roleRepository.GetByIdAsync(roleId).ConfigureAwait(false);

                permissionsToGet = role.Permissions;
            }

            var permissions = await _permissionRepository.GetAllAsync(p =>
            p.IsPublic != isPrivate &&
            (string.IsNullOrEmpty(roleId) || permissionsToGet.Contains(p.Id)))
                .ConfigureAwait(false);

            return base._mapper.Map<List<Permission>>(permissions);
        }
    }
}
