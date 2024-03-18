using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Authorizations
{
    public sealed record class RolePermission
    {
        public string RoleId { get; set; } = null!;

        public string PermissionId { get; set; } = null!;

        public RoleEfCore Role { get; set; } = null!;

        public PermissionEfCore Permission { get; set; } = null!;

        public RolePermission()
        {
        }

        public RolePermission(
            string roleId,
            string permissionId)
        {
            this.RoleId = roleId;
            this.PermissionId = permissionId;
        }
    }
}
