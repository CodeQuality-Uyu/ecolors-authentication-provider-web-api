using CQ.AuthProvider.BusinessLogic.Authorizations;
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
    }
}
