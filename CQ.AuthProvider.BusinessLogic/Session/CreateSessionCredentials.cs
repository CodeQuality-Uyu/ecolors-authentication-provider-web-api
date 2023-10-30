using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class CreateSessionCredentials
    {
        public string Email { get; }

        public string Password { get; }

        public CreateSessionCredentials(string email, string password)
        {
            Email = Guard.Encode(email.Trim());
            Password = Guard.Encode(password.Trim());

            Guard.ThrowIsInvalidEmailFormat(Email);
        }
    }
}
