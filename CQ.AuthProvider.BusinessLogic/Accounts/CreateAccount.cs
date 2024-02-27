using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public record class CreateAccount
    {
        public readonly string Email;

        public readonly string Password;

        public readonly string? FirstName;

        public readonly string? LastName;

        public readonly RoleKey Role;

        public CreateAccount(
            string email, 
            string password, 
            string firstName, 
            string lastName,
            string role)
        {
            this.Email = Guard.Encode(email.Trim());
            this.Password = Guard.Encode(password.Trim());
            this.FirstName = Guard.Encode(firstName.Trim());
            this.LastName = Guard.Encode(lastName.Trim());
            this.Role = new(role);

            Guard.ThrowIsInputInvalidEmail(this.Email);

            Guard.ThrowIsInputInvalidPassword(this.Password);
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
