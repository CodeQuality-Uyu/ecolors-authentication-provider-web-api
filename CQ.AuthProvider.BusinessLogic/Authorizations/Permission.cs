using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed class Permission
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Key { get; set; } = null!;

        public bool IsPublic { get; set; }

        public Permission()
        {
            Id = Db.NewId();
        }

        public Permission(
            string name,
            string description,
            PermissionKey key,
            bool isPublic)
            : this()
        {
            this.Name = name;
            this.Description = description;
            this.Key = key.ToString();
            this.IsPublic = isPublic;
        }
    }
}
