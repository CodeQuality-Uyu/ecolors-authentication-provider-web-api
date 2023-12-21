using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerFinder.Auth.Core.Exceptions
{
    public class CodesNotMatchException : Exception
    {
        public readonly string Code;

        public readonly string Email;

        public CodesNotMatchException(string code, string email) 
        { 
            this.Code = code;
            this.Email = email;
        }
    }
}
