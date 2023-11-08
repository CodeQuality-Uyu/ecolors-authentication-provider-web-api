using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Exceptions
{
    public class ResourceNotFoundException : Exception 
    {
        public string? Resource => (string)Data["Resource"];
    }
}
