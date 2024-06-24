using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.Mongo.Authorizations
{
    public sealed record class RoleMongo
    {
        public string Id { get; init; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Key { get; init; } = null!;

        public List<string> Permissions { get; set; } = null!;

        public bool IsPublic { get; set; }

        public bool IsDefault { get; set; }

        public RoleMongo()
        {
            Id = Db.NewId();
            Permissions = new List<string>();
        }

        public RoleMongo(
            string name,
            string description,
            RoleKey key,
            List<PermissionKey> permissions,
            bool isPublic = false,
            bool isDefault = false)
            : this()
        {
            Name = name;
            Description = description;
            Key = key.ToString();
            Permissions = permissions.Select(p => p.ToString()).ToList();
            IsPublic = isPublic;
            IsDefault = isDefault;
        }
    }
}
