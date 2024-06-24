using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public sealed record class AccountMongo
    {
        public string Id { get; set; } = null!;

        public string? ProfilePictureUrl { get; set; }

        public string FullName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public List<MiniRoleMongo> Roles { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public AccountMongo()
        {
            Id = Db.NewId();
            Roles = new List<MiniRoleMongo>();
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public AccountMongo(
            string fullName,
            string firstName,
            string lastName,
            string email,
            MiniRoleMongo role,
            string? profilePictureUrl = null
            )
            : this()
        {
            FullName = fullName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Roles = new List<MiniRoleMongo> { role };
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}
