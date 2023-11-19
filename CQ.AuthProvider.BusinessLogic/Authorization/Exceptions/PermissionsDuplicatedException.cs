using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed class PermissionsDuplicatedException : Exception
    {
        public readonly IList<string> Keys;

        public PermissionsDuplicatedException(IList<string> keys)
        {
            this.Keys = keys;
        }
    }
}
