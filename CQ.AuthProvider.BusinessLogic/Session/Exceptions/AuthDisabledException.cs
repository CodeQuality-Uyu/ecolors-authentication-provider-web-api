using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public class AuthDisabledException : Exception
    {
        public string Email { get; }

        public AuthDisabledException(string email) 
        {
            Email = email;
        }
    }
}
