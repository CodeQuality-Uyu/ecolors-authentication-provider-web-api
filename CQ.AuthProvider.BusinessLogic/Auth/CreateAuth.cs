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
        public readonly string Email;

        public readonly string Password;

        public readonly string? FirstName;

        public readonly string? LastName;

        public readonly Roles Role;

        public CreateAuth(
            string email, 
            string password, 
            string firstName, 
            string lastName,
            string role)
        {
            Email = Guard.Encode(email.Trim());
            Password = Guard.Encode(password.Trim());
            FirstName = Guard.Encode(firstName.Trim());
            LastName = Guard.Encode(lastName.Trim());
            Role = new(role);

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
