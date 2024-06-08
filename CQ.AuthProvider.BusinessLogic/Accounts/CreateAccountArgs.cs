using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public sealed record class CreateAccountArgs
    {
        public readonly string Email = null!;

        public readonly string Password = null!;

        public readonly string FirstName = null!;

        public readonly string LastName = null!;

        public readonly string? ProfilePictureUrl;

        public string FullName => $"{FirstName} {LastName}";

        public readonly RoleKey? Role;

        public CreateAccountArgs(
            string email,
            string password,
            string firstName,
            string lastName,
            string? role,
            string? profilePictureUrl)
        {
            Email = Guard.Encode(email, nameof(email));
            Password = Guard.Encode(password, nameof(password));
            FirstName = Guard.Normalize(Guard.Encode(firstName, nameof(firstName)));
            LastName = Guard.Normalize(Guard.Encode(lastName, nameof(lastName)));
            Role = Guard.IsNotNullOrEmpty(role) ? new(role) : null;

            Guard.ThrowIsInputInvalidEmail(Email);

            Guard.ThrowIsInputInvalidPassword(Password);

            Guard.ThrowIsNullOrEmpty(FirstName, nameof(FirstName));
            Guard.ThrowIsNullOrEmpty(LastName, nameof(LastName));

            ProfilePictureUrl = profilePictureUrl;
        }
    }
}
