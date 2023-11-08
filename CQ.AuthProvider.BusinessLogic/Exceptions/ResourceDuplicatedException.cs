using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Exceptions
{
    public sealed class ResourceDuplicatedException : Exception
    {
        public readonly string PropDuplicated;

        public ResourceDuplicatedException(string propDuplicated)
        {
            PropDuplicated = propDuplicated;
        }
    }
}
