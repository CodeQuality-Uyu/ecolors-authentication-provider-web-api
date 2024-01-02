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
        public readonly string Email;

        public readonly string Password;

        public CreateSessionCredentials(string email, string password)
        {
            this.Email = Guard.Encode(email.Trim());
            this.Password = Guard.Encode(password.Trim());

            Guard.ThrowIsInvalidEmailFormat(this.Email);
        }
    }
}
