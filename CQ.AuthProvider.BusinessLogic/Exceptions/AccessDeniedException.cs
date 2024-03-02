using CQ.AuthProvider.BusinessLogic.Authorizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Exceptions
{
    public sealed class AccessDeniedException : Exception
    {
        public readonly PermissionKey PermissionKey;

        public AccessDeniedException(PermissionKey permissionKey)
        {
            this.PermissionKey = permissionKey;
        }
    }
}
