using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    internal sealed record class LoginFirebaseError
    {
        public LoginError Error { get; set; }
    }

    internal sealed record class LoginError
    {
        public string Code { get; set; }

        public FirebaseAuthErrorCode AuthCode { get { return new FirebaseAuthErrorCode(Message); } }

        public string Message { get; set; }
    }
}
