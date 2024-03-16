using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations.Exceptions
{
    public class PermissionNotFoundException : Exception
    {
        public readonly List<PermissionKey> PermissionKeys;

        public PermissionNotFoundException(List<PermissionKey> permissionKeys)
        {
            PermissionKeys = permissionKeys;
        }
    }
}
