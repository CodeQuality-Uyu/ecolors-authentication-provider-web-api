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
            var firstNameNormalize = Normalize(this.FirstName);
            var lastNameNormalize = Normalize(this.LastName);

            var fullName = $"{firstNameNormalize} {lastNameNormalize}";

            var withoutSpace = fullName.Trim();

            if (string.IsNullOrEmpty(withoutSpace))
            {
                return null;
            }

            return withoutSpace;
        }

        private string? Normalize(string? input)
        {
            var normalized = !string.IsNullOrEmpty(input) ? $"{char.ToUpper(input[0])}{input[1..]}" : null;

            return normalized;
        }
    }
}
