using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.Mongo.Accounts.Exceptions
{
    public sealed class AuthNotFoundException : Exception
    {
        public string Email { get; }

        public AuthNotFoundException(string email) { Email = email; }

        public AuthNotFoundException(string email, Exception innerException) : base(innerException.Message, innerException) { Email = email; }
    }
}
