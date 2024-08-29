using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions.Exceptions
{
    public sealed class InvalidCredentialsException : Exception
    {
        public string Email { get; }

        public InvalidCredentialsException(string email)
        {
            Email = email;
        }
    }
}
