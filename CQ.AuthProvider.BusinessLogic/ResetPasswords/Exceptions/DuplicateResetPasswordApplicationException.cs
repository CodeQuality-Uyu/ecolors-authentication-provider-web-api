using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords.Exceptions
{
    public sealed class DuplicateResetPasswordApplicationException : Exception
    {
        public readonly string Email;

        public DuplicateResetPasswordApplicationException(string email)
        {
            Email = email;
        }
    }
}
