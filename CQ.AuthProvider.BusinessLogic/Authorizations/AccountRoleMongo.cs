using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class AccountRoleMongo
    {
        public string Key { get; set; } = null!;

        public List<string> Permissions { get; set; } = null!;

        public AccountRoleMongo()
        {
            this.Permissions = new List<string>();
        }

        public AccountRoleMongo(
            string key,
            List<string> permissions)
        {
            this.Key = key;
            this.Permissions = permissions;
        }
    }
}
