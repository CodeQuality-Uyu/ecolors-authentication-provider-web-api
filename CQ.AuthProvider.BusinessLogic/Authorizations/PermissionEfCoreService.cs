using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Exceptions;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal sealed class PermissionEfCoreService : PermissionService<PermissionEfCore>
    {
        private readonly IRepository<PermissionEfCore> _permissionRepository;
        public PermissionEfCoreService(
            IRepository<PermissionEfCore> permissionRepository,
            IMapper mapper) : base(mapper)
        {
            this._permissionRepository = permissionRepository;
        }

        #region Create
        protected override async Task<bool> ExistByKeyAsync(PermissionKey permissionKey)
        {
            var permissionValue = permissionKey.ToString();

            var existPermission = await this._permissionRepository.ExistAsync(p => p.Key == permissionValue).ConfigureAwait(false);

            return existPermission;
        }

        protected override async Task CreateAsync(CreatePermission newPermission)
        {
            var permission = new PermissionEfCore(
                newPermission.Name,
                newPermission.Description,
                newPermission.Key,
                newPermission.IsPublic);

            await this._permissionRepository.CreateAsync(permission).ConfigureAwait(false);
        }
        #endregion

        #region CreateBulk
        protected override async Task<List<PermissionEfCore>> GetAllByPermissionKeyAsync(List<string> permissions)
        {
            var permissionsSaved = await this._permissionRepository
                .GetAllAsync(p => permissions.Contains(p.Key))
                .ConfigureAwait(false);

            return permissionsSaved;
        }

        protected override async Task CreateBulkAsync(List<CreatePermission> permissions)
        {
            var permissionsMapped = permissions
                .Select(p => new PermissionEfCore(
                p.Name,
                p.Description,
                p.Key,
                p.IsPublic))
                .ToList();

            await this._permissionRepository.CreateBulkAsync(permissionsMapped).ConfigureAwait(false);
        }
        #endregion

        protected override async Task<List<Permission>> GetAllAsync(Account accountLogged, bool isPrivate = false, string? roleId = null)
        {
            var permissions = await 
                this._permissionRepository.GetAllAsync(p =>
                p.IsPublic != isPrivate &&
                (string.IsNullOrEmpty(roleId) || p.Roles.Any(r => r.Id == roleId)))
                .ConfigureAwait(false);

            return base._mapper.Map<List<Permission>>(permissions);
        }
    }
}
