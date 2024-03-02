using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class RoleEfCore
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Key { get; set; } = null!;

        public List<PermissionEfCore> Permissions { get; set; } = null!;

        public List<AccountEfCore> Accounts { get; set; } = null!;

        public bool IsPublic { get; set; }

        public RoleEfCore()
        {
            this.Id = Db.NewId();
            this.Permissions = new List<PermissionEfCore>();
            this.Accounts = new List<AccountEfCore>();
        }

        public RoleEfCore(
            string name,
            string description,
            RoleKey key,
            List<PermissionEfCore> permissions,
            bool isPublic)
            : this()
        {
            Name = name;
            Description = description;
            Key = key.ToString();
            Permissions = permissions;
            IsPublic = isPublic;
        }
    }
}
