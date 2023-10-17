using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public class InvalidCredentialsException : Exception
    {
        public string Origin { get; }

        public string Email { get; }

        public InvalidCredentialsException(string email, FirebaseAuthErrorCode code)
        {
            Email = email;
            Origin = code.Value;
        }
    }
}
