using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class RoleMongo
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Key { get; set; } = null!;

        public List<string> Permissions { get; set; } = null!;

        public bool IsPublic { get; set; }

        public RoleMongo()
        {
            this.Id = Db.NewId();
            this.Permissions = new List<string>();
        }

        public RoleMongo(
            string name,
            string description,
            RoleKey key,
            List<PermissionKey> permissions,
            bool isPublic)
            : this()
        {
            Name = name;
            Description = description;
            Key = key.ToString();
            Permissions = permissions.Select(p => p.ToString()).ToList();
            IsPublic = isPublic;
        }
    }
}
