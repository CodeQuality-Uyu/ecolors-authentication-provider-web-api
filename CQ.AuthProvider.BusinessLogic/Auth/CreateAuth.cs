using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public record CreateAuth
    {
        public string Email { get; }

        public string Password { get; }

        public string? FirstName { get; }

        public string? LastName { get; }

        public CreateAuth(
            string? email, 
            string? password, 
            string? firstName, 
            string? lastName)
        {
            Email = Guard.Encode(email?.Trim() ?? string.Empty);
            Password = Guard.Encode(password?.Trim() ?? string.Empty);
            FirstName = Guard.Encode(firstName?.Trim() ?? string.Empty);
            LastName = Guard.Encode(lastName?.Trim() ?? string.Empty);

            Guard.ThrowIsNullOrEmpty(Email, "email");
            Guard.ThrowEmailFormat(Email);

            Guard.ThrowIsNullOrEmpty(Password, "password");
            Guard.ThrowMinimumLength(Password, 8, "password");
            Guard.ThrowPasswordFormat(Password);
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
