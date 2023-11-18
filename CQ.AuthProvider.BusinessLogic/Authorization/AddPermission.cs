using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class AddPermission
    {
        public readonly IList<string> PermissionsKeys;

        public AddPermission(IList<string> permissionsKeys)
        {
            this.PermissionsKeys = permissionsKeys;

            if (!this.PermissionsKeys.Any()) throw new ArgumentException("Must contain at least one elmeent", nameof(this.PermissionsKeys));
        }
    }
}
