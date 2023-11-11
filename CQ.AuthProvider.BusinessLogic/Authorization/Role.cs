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
        public string Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string Key { get; init; }

        public IList<string> PermissionKeys { get; init; }

        public bool IsPublic { get; init; }

        public Role()
        {
            Id = Db.NewId();
        }

        public Role(string name, string description, string key, IList<string> permissions, bool isPublic)
        {
            Id = Db.NewId();
            Name = name;
            Description = description;
            Key = key;
            PermissionKeys = permissions;
            IsPublic = isPublic;
        }

        public bool HasPermission(string permission)
        {
            return PermissionKeys.Contains(permission);
        }
    }
}
