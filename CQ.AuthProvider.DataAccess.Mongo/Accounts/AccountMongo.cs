using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.Mongo.Accounts
{
    public sealed record class AccountMongo
    {
        public string Id { get; init; } = null!;

        public string? ProfilePictureUrl { get; set; }

        public string FullName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public List<MiniAcountRole> Roles { get; set; } = [];

        public string Locale { get; set; } = null!;

        public string TimeZone { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// For MongoDriver
        /// </summary>
        public AccountMongo()
        {
        }

        /// <summary>
        /// For new AccountMongo
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <param name="profilePictureUrl"></param>
        public AccountMongo(
            string id,
            string email,
            string fullName,
            string firstName,
            string lastName,
            string locale,
            string timeZone,
            List<Role> roles,
            string? profilePictureUrl
            )
        {
            Id = id;
            Email = email;
            FullName = fullName;
            FirstName = firstName;
            LastName = lastName;
            Locale = locale;
            TimeZone = timeZone;
            Roles = roles.ConvertAll(r => new MiniAcountRole(r.Permissions));
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}
