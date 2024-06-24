using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords.Exceptions
{
    public class CodesNotMatchException : Exception
    {
        public readonly string Code;

        public readonly string Email;

        public CodesNotMatchException(string code, string email)
        {
            Code = code;
            Email = email;
        }
    }
}
