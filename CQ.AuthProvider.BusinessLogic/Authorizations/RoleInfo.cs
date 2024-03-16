using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class RoleInfo
    {
        public string Id { get; init; } = null!; 

        public string Name { get; init; } = null!;

        public string Description { get; init; } = null!;

        public bool IsPublic { get; init; }

        public RoleKey Key { get; init; } = null!;

        public List<PermissionKey> Permissions { get; init; } = null!;
    }
}
