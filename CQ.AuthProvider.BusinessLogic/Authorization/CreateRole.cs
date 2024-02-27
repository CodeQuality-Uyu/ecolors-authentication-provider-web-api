using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class CreateRole
    {
        public readonly string Name;

        public readonly string Description;

        public readonly string Key;

        public readonly List<string> PermissionKeys;

        public readonly bool IsPublic;

        public CreateRole(
            string name,
            string description,
            string key,
            List<string> permissionKeys,
            bool isPublic)
        {
            this.Name = Guard.Encode(name.Trim());
            this.Description = Guard.Encode(description.Trim());
            this.Key= Guard.Encode(key.Trim());
            this.PermissionKeys = permissionKeys;
            this.IsPublic = isPublic;

            Guard.ThrowIsNullOrEmpty(this.Name, nameof(this.Name));
            Guard.ThrowIsMoreThan(this.Name, 50,nameof(this.Name));

            Guard.ThrowIsNullOrEmpty(this.Description, nameof(this.Description));
            Guard.ThrowIsMoreThan(this.Description, 200,nameof(this.Description));

            Guard.ThrowIsNullOrEmpty(this.Key, nameof(this.Key));
            Guard.ThrowIsMoreThan(this.Key, 50,nameof(this.Key));

            if (!this.PermissionKeys.Any())
            {
                throw new ArgumentException("Missing at least one permission", nameof(permissionKeys));
            }
        }
    }
}
