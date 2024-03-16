using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public sealed record class AccountEfCore
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public List<RoleEfCore> Roles { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public AccountEfCore()
        {
            this.Id = Db.NewId();
            this.CreatedAt = DateTimeOffset.UtcNow;
            this.Roles = new List<RoleEfCore>();
        }

        public AccountEfCore(
            string email,
            string fullName,
            string firstName,
            string lastName,
            RoleEfCore role)
            : this()
        {
            this.Email = email;
            this.FullName = fullName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Roles = new List<RoleEfCore> { role };
        }
    }
}
