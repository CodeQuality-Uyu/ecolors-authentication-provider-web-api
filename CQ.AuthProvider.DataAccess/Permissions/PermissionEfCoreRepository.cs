using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Authorizations.Mappings;
using CQ.UnitOfWork.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.Permissions
{
    internal sealed class PermissionEfCoreRepository : EfCoreRepository<PermissionEfCore>, IPermissionRepository<PermissionEfCore>
    {
        private readonly IMapper _mapper;

        public PermissionEfCoreRepository(EfCoreContext efCoreContext) : base(efCoreContext)
        {
            var config = new MapperConfiguration(conf => conf.AddProfile<PermissionProfile>());
            this._mapper = config.CreateMapper();
        }

        public async Task<bool> ExistByKeyAsync(PermissionKey permissionKey)
        {
            return await base.ExistAsync(p => p.Key == permissionKey.ToString()).ConfigureAwait(false);
        }

        public async Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> permissionKeys)
        {
            var basicPermissions = this._mapper.Map<List<string>>(permissionKeys);

            var permissions = await base.GetAllAsync<Permission>(p => basicPermissions.Contains(p.Key)).ConfigureAwait(false);

            return permissions;
        }

        public async Task<List<Permission>> GetAllInfoAsync()
        {
            return await base.GetAllAsync<Permission>().ConfigureAwait(false);
        }
    }
}
