using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed class Permission
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Key { get; set; } = null!;

        public bool IsPublic { get; set; }

        public List<Role> Roles { get; set; } = null!;

        public Permission()
        {
            Id = Db.NewId();
            this.Roles = new List<Role>();
        }

        public Permission(
            string name,
            string description,
            string key,
            bool isPublic)
            : this()
        {
            this.Name = name;
            this.Description = description;
            this.Key = key;
            this.IsPublic = isPublic;
        }
    }
}
