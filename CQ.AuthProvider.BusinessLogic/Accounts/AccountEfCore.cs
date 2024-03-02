using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public sealed record class AccountEfCore
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

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
            string name,
            RoleEfCore role)
            : this()
        {
            this.Email = email;
            this.Name = name;
            this.Roles = new List<RoleEfCore> { role };
        }
    }
}
