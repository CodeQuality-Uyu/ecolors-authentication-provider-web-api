using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public sealed record class AccountMongo
    {
        public string Id { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public List<MiniRoleMongo> Roles { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public AccountMongo()
        {
            this.Id = Db.NewId();
            this.Roles = new List<MiniRoleMongo>();
            this.CreatedAt = DateTimeOffset.UtcNow;
        }

        public AccountMongo(
            string fullName,
            string firstName,
            string lastName,
            string email,
            MiniRoleMongo role
            )
            : this()
        {
            this.FullName = fullName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email= email;
            this.Roles = new List<MiniRoleMongo> { role };
        }
    }
}
