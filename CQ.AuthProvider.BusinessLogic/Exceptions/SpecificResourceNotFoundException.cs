using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Exceptions
{
    public class SpecificResourceNotFoundException<TResource> : ResourceNotFoundException 
        where TResource : class
    {
        public SpecificResourceNotFoundException(string key, string value) : base(key, value, nameof(TResource)) { }
    }
}
