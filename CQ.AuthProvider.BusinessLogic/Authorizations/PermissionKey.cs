using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class PermissionKey
    {
        public static readonly PermissionKey GetPrivateRoles = new("getprivate-role");
        public static readonly PermissionKey Any = new("*");

        private readonly string Value;

        public PermissionKey(string value)
        {
            this.Value = Guard.Encode(value.Trim())!;

            Guard.ThrowIsNullOrEmpty(this.Value, nameof(this.Value));
        }

        public override string ToString() => this.Value;
    }
}
