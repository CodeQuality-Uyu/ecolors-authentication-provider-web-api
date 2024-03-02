using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations.Exceptions
{
    public sealed class PermissionsDuplicatedException : Exception
    {
        public readonly List<PermissionKey> Keys;

        public PermissionsDuplicatedException(List<PermissionKey> keys)
        {
            this.Keys = keys;
        }
    }
}
