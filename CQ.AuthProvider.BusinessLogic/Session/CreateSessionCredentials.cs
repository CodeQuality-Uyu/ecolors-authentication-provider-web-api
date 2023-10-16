using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public class CreateSessionCredentials
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public CreateSessionCredentials(string email, string password)
        {
            Email = Guard.Encode(email.Trim());
            Password = Guard.Encode(password.Trim());

            Guard.ThrowIsInvalidEmailFormat(Email);
            Guard.ThrowIsInputInvalidPassword(Password);
        }
    }
}
