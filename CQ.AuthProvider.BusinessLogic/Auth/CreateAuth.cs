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
            Email = email?.Trim() ?? string.Empty;
            Password = password?.Trim() ?? string.Empty;
            FirstName = firstName?.Trim() ?? string.Empty;
            LastName = lastName?.Trim() ?? string.Empty;
        }

        public string FullName()
        {
            var fullName = $"{this.FirstName} {this.LastName}";

            var withoutSpace = fullName.Trim();

            if (string.IsNullOrEmpty(withoutSpace))
            {
                return string.Empty;
            }

            return withoutSpace;
        }
    }
}
