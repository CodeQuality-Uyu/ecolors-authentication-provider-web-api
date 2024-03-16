using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class AddPermission
    {
        public readonly List<PermissionKey> PermissionsKeys;

        public AddPermission(List<string> permissionsKeys)
        {
            this.PermissionsKeys = permissionsKeys.Select(p => new PermissionKey(p)).ToList();

            if (!this.PermissionsKeys.Any()) throw new ArgumentException("Must contain at least one elmeent", nameof(this.PermissionsKeys));
        }
    }
}
