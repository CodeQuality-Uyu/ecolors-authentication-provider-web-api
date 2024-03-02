using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class CreatePermission
    {
        public readonly string Name;

        public readonly string Description;

        public readonly PermissionKey Key;

        public readonly bool IsPublic;

        public CreatePermission(
            string name,
            string description,
            string key,
            bool isPublic)
        {
            this.Name = Guard.Encode(name.Trim());
            this.Description = Guard.Encode(description.Trim());
            this.Key = new PermissionKey(key);
            this.IsPublic = isPublic;

            Guard.ThrowIsNullOrEmpty(this.Name, nameof(this.Name));
            Guard.ThrowIsMoreThan(this.Name, 50, nameof(this.Name));

            Guard.ThrowIsNullOrEmpty(this.Description, nameof(this.Description));
            Guard.ThrowIsMoreThan(this.Description, 200, nameof(this.Description));
        }
    }
}
