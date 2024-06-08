using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public sealed record class AccountEfCore
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? ProfilePictureUrl { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public List<RoleEfCore> Roles { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public AccountEfCore()
        {
            Id = Db.NewId();
            CreatedAt = DateTimeOffset.UtcNow;
            Roles = new List<RoleEfCore>();
        }

        public AccountEfCore(
            string email,
            string fullName,
            string firstName,
            string lastName,
            string roleId,
            string? profilePictureUrl = null)
            : this()
        {
            Email = email;
            FullName = fullName;
            FirstName = firstName;
            LastName = lastName;
            Roles = new List<RoleEfCore> { new() { Id = roleId } };
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}
