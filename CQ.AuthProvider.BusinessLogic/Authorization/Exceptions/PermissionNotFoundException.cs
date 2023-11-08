using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorization.Exceptions
{
    public class PermissionNotFoundException : Exception
    {
        public readonly IList<string> PermissionKeys;

        public PermissionNotFoundException(IList<string> permissionKeys)
        {
            PermissionKeys = permissionKeys;
        }
    }
}
