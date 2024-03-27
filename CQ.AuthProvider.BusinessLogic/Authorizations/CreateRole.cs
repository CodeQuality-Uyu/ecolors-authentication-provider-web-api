using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class CreateRole
    {
        public readonly string Name;

        public readonly string Description;

        public readonly RoleKey Key;

        public readonly List<PermissionKey> PermissionKeys;

        public readonly bool IsPublic;

        public readonly bool IsDefault;

        public CreateRole(
            string name,
            string description,
            string key,
            List<string> permissionKeys,
            bool isPublic,
            bool isDefault)
        {
            this.Name = Guard.Encode(name);
            this.Description = Guard.Encode(description);
            this.Key= new RoleKey(key);
            this.PermissionKeys = permissionKeys.Select(p => new PermissionKey(p)).ToList();
            this.IsPublic = isPublic;
            this.IsDefault = isDefault;

            Guard.ThrowIsMoreThan(this.Name, 50,nameof(this.Name));

            Guard.ThrowIsMoreThan(this.Description, 200,nameof(this.Description));
        }
    }
}
