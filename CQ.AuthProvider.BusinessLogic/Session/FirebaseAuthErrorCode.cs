using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public record class FirebaseAuthErrorCode
    {
        public static FirebaseAuthErrorCode EmailNotFound = new("EMAIL_NOT_FOUND");

        public static FirebaseAuthErrorCode InvalidPassword = new("INVALID_PASSWORD");

        public static FirebaseAuthErrorCode UserDisabled = new("USER_DISABLED");

        public string Value { get; }

        public FirebaseAuthErrorCode(string value)
        {
            this.Value = value;
        }
    }
}
