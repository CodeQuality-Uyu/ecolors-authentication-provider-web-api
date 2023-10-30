using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed class AuthDisabledException : Exception
    {
        public string Email { get; }

        public AuthDisabledException(string email) 
        {
            Email = email;
        }
    }
}
