using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Exceptions;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public sealed record class Account
    {
        public string Id { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string FullName { get; init; } = null!;

        public string FirstName { get; init; } = null!;

        public string LastName { get; init; } = null!;

        public List<RoleKey> Roles { get; init; } = null!;

        public List<PermissionKey> Permissions { get; init; } = null!;

        public void AssertPermission(PermissionKey permission)
        {
            var hasPermission = this.Permissions.Any(p => p == permission || p == PermissionKey.Joker);

            if (!hasPermission)
                throw new AccessDeniedException(permission.ToString());
        }

        public bool HasPermission(PermissionKey permission)
        {
            return this.Permissions.Contains(permission);
        }
    }
}
