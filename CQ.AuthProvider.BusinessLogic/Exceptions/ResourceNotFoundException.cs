using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Exceptions
{
    public abstract class ResourceNotFoundException : Exception 
    {
        public readonly string Key;

        public readonly string Value;

        public readonly string Resource;

        public ResourceNotFoundException(string  key, string value, string resource)
        {
            this.Key = key;
            this.Value = value;
            this.Resource = resource;
        }
    }
}
