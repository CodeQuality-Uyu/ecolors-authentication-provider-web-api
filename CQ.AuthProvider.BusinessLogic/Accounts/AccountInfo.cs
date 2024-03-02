using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    public sealed record class AccountInfo
    {
        public string Id { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string Name { get; init; } = null!;

        public List<RoleKey> Roles { get; init; } = null!;

        public List<PermissionKey> Permissions { get; init; } = null!;

        public void AssertPermission(PermissionKey permission)
        {
            var hasPermission = this.Permissions.Any(p => p == permission || p == PermissionKey.Joker);

            if (!hasPermission)
                throw new AccessDeniedException(permission.ToString());
        }
    }
}
