using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.Mongo.Authorizations
{
    public sealed record class MiniRoleMongo
    {
        public string Key { get; init; } = null!;

        public List<string> Permissions { get; init; } = null!;

        public MiniRoleMongo()
        {
        }

        public MiniRoleMongo(
            RoleKey key,
            List<PermissionKey> permissions)
        {
            Key = key.ToString();
            Permissions = permissions.Select(p => p.ToString()).ToList();
        }
    }
}
