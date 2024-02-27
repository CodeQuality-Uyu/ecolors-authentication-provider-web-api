using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class Role
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Key { get; set; } = null!;

        public List<Permission> Permissions { get; set; } = null!;

        public List<Account> Accounts { get; set; } = null!;

        public bool IsPublic { get; set; }

        public Role()
        {
            this.Id = Db.NewId();
            this.Permissions = new List<Permission>();
            this.Accounts = new List<Account>();
        }

        public Role(
            string name,
            string description,
            string key,
            List<Permission> permissions,
            bool isPublic)
            : this()
        {
            Name = name;
            Description = description;
            Key = key;
            Permissions = permissions;
            IsPublic = isPublic;
        }

        public bool HasPermission(string permission)
        {
            return this.Permissions.Any(p => p.Key == permission) || this.Permissions.Any(p => p.Key == PermissionKey.Any.ToString());
        }
    }
}
