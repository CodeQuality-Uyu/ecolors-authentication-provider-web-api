using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class Permission
    {
        public string Id { get; init; } = null!;

        public string Name { get; init; } = null!;

        public string Description { get; init; } = null!;

        public bool IsPublic { get; init; }

        public PermissionKey Key { get; init; } = null!;
    }
}
