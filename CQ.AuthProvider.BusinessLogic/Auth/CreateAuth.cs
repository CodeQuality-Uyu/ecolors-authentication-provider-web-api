using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public record class CreateAuth
    {
        public string Email { get; }

        public string Password { get; }

        public string? FirstName { get; }

        public string? LastName { get; }

        public CreateAuth(
            string email, 
            string password, 
            string firstName, 
            string lastName)
        {
            Email = Guard.Encode(email.Trim());
            Password = Guard.Encode(password.Trim());
            FirstName = Guard.Encode(firstName.Trim());
            LastName = Guard.Encode(lastName.Trim());

            Guard.ThrowIsInputInvalidEmail(Email);

            Guard.ThrowIsInputInvalidPassword(Password);
        }

        public string? FullName()
        {
            var fullName = $"{this.FirstName} {this.LastName}";

            var withoutSpace = fullName.Trim();

            if (string.IsNullOrEmpty(withoutSpace))
            {
                return null;
            }

            return withoutSpace;
        }
    }
}
