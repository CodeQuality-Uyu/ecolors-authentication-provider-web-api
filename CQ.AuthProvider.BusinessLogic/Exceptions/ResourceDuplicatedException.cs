using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Exceptions
{
    public sealed class ResourceDuplicatedException: Exception
    {
        public readonly string Key;

        public readonly string Value;

        public readonly string Resource;

        public ResourceDuplicatedException(string key, string value, string resource)
        {
            Key = key;
            Value = value;
            Resource = resource;
        }
    }
}
