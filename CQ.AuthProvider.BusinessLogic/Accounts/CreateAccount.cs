using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public record class CreateAccount
    {
        public readonly string Email = null!;

        public readonly string Password = null!;

        public readonly string FirstName = null!;

        public readonly string LastName = null!;

        public string FullName => $"{this.FirstName} {this.LastName}";

        public readonly RoleKey Role;

        public CreateAccount(
            string email, 
            string password, 
            string firstName, 
            string lastName,
            string role)
        {
            this.Email = Guard.Encode(email.Trim())!;
            this.Password = Guard.Encode(password.Trim())!;
            this.FirstName = Normalize(Guard.Encode(firstName.Trim())!);
            this.LastName = Normalize(Guard.Encode(lastName.Trim())!);
            this.Role = new(role);

            Guard.ThrowIsInputInvalidEmail(this.Email);

            Guard.ThrowIsInputInvalidPassword(this.Password);

            Guard.ThrowIsNullOrEmpty(this.FirstName, nameof(this.FirstName));
            Guard.ThrowIsNullOrEmpty(this.LastName, nameof(this.LastName));
        }

        private static string Normalize(string input)
        {
            if (input.Length == 0)
                return input;

            if(input.Length == 1)
                return $"{char.ToUpper(input[0])}";

            return $"{char.ToUpper(input[0])}{input[1..]}";
        }
    }
}
