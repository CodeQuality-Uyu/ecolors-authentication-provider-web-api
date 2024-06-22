using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts
{
    public readonly struct CreateAccountArgs
    {
        public string Email { get; init; }

        public string Password { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Locale { get; init; }

        public string TimeZone { get; init; }

        public string? ProfilePictureUrl { get; init; }

        public string FullName { get; init; }

        public readonly RoleKey? Role { get; init; }

        public CreateAccountArgs(
            string email,
            string password,
            string firstName,
            string lastName,
            string locale,
            string timeZone,
            string? role,
            string? profilePictureUrl)
        {
            Email = Guard.Encode(email, nameof(email));
            Guard.ThrowIsInputInvalidEmail(Email);

            Password = Guard.Encode(password, nameof(password));
            Guard.ThrowIsInputInvalidPassword(Password);

            FirstName = Guard.Normalize(Guard.Encode(firstName, nameof(firstName)));
            LastName = Guard.Normalize(Guard.Encode(lastName, nameof(lastName)));
            FullName = $"{FirstName} {LastName}";

            Locale = Guard.Encode(locale, nameof(locale));
            TimeZone = Guard.Encode(timeZone, nameof(timeZone));

            Role = Guard.IsNotNullOrEmpty(role) ? new(role) : null;
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}
