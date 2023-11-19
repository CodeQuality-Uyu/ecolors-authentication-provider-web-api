using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public sealed record class CreatePermission
    {
        public readonly string Name;

        public readonly string Description;

        public readonly string Key;

        public readonly bool IsPublic;

        public CreatePermission(string name, string description, string key, bool isPublic)
        {
            this.Name = Guard.Encode(name.Trim());
            this.Description = Guard.Encode(description.Trim());
            this.Key = key;
            this.IsPublic = isPublic;

            Guard.ThrowIsNullOrEmpty(this.Name, nameof(this.Name));
            Guard.ThrowIsMoreThan(this.Name, 50, nameof(this.Name));

            Guard.ThrowIsNullOrEmpty(this.Description, nameof(this.Description));
            Guard.ThrowIsMoreThan(this.Description, 200, nameof(this.Description));

            Guard.ThrowIsNullOrEmpty(this.Key, nameof(this.Key));
            Guard.ThrowIsMoreThan(this.Key, 50, nameof(this.Key));
        }
    }
}
