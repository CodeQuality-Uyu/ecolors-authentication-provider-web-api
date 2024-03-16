using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal sealed class PermissionEfCoreService : PermissionService<PermissionEfCore>
    {
        public PermissionEfCoreService(IPermissionRepository<PermissionEfCore> permissionRepository) : base(permissionRepository)
        {
        }

        protected override async Task SaveNewPermissionAsync(CreatePermission newPermission)
        {
            var permission = new PermissionEfCore(
                newPermission.Name,
                newPermission.Description,
                newPermission.Key,
                newPermission.IsPublic);

            await base._permissionRepository.CreateAsync(permission).ConfigureAwait(false);
        }
    }
}
