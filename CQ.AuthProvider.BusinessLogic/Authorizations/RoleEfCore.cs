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
        public string Id { get; init; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Key { get; init; } = null!;

        public List<PermissionEfCore> Permissions { get; set; } = null!;

        public List<AccountEfCore> Accounts { get; set; } = null!;

        public bool IsPublic { get; set; }

        public bool IsDefault {  get; set; }

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
            bool isPublic = false,
            bool isDefault = false)
            : this()
        {
            Name = name;
            Description = description;
            Key = key.ToString();
            Permissions = permissions;
            IsPublic = isPublic;
            IsDefault = isDefault;
        }
    }
}
