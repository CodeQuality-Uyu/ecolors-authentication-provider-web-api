using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    internal sealed class PermissionMongoService : PermissionService<Permission>
    {
        public PermissionMongoService(IPermissionRepository<Permission> permissionRepository) : base(permissionRepository)
        {
        }

        protected override async Task SaveNewPermissionAsync(CreatePermission newPermission)
        {
            var permission = new Permission(
                newPermission.Name,
                newPermission.Description,
                newPermission.Key.ToString(),
                newPermission.IsPublic);

            await base._permissionRepository.CreateAsync(permission).ConfigureAwait(false);
        }
    }
}
