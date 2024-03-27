using CQ.AuthProvider.BusinessLogic.Authorizations;

namespace CQ.AuthProvider.BusinessLogic.Sessions
{
    public sealed record class SessionCreated
    {
        public readonly string AccountId;

        public readonly string Email;

        public readonly string FirstName;

        public readonly string LastName;

        public readonly string FullName;

        public readonly string Token;

        public readonly List<RoleKey> Roles;

        public readonly List<PermissionKey> Permissions;

        public SessionCreated(
            string accountId,
            string email,
            string token,
            string firstName,
            string lastName,
            string fullName,
            List<RoleKey> roles,
            List<PermissionKey> permissions)
        {
            this.AccountId = accountId;
            this.Email = email;
            this.Token = token;
            this.Roles = roles;
            this.Permissions = permissions;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = fullName;
        }
    }
}
