using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class MiniRoleMongo
    {
        public string Key { get; init; } = null!;

        public List<string> Permissions { get; init; } = null!;

        public MiniRoleMongo()
        {
        }

        public MiniRoleMongo(
            string key,
            List<string> permissions)
        {
            this.Key = key;
            this.Permissions = permissions;
        }
    }
}
