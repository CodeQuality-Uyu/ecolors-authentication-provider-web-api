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
        #region Permission
        public static readonly PermissionKey CreatePermission = new("create-permission");
        public static readonly PermissionKey GetByIdPermission= new("getbyid-permission");
        public static readonly PermissionKey GetAllPermissions = new("getall-permission");
        public static readonly PermissionKey GetAllPrivatePermissions = new("getallprivate-permission");
        public static readonly PermissionKey GetAllPermissionsByRoleId = new("getallbyroleid-permission");
        public static readonly PermissionKey UpdateByIdPermission = new("updatebyid-permission");
        #endregion
        
        #region Region
        public static readonly PermissionKey CreateRole = new("create-role");
        public static readonly PermissionKey GetByIdRole = new("getbyid-role");
        public static readonly PermissionKey GetAllRoles = new("getall-role");
        public static readonly PermissionKey GetAllPrivateRoles = new("getallprivate-role");
        public static readonly PermissionKey AddPermissionToRole = new("addpermission-role");
        #endregion
        
        public static readonly PermissionKey Joker = new("*");
        
        private readonly string Value;

        public PermissionKey(string value)
        {
            this.Value = Guard.Encode(value.Trim())!;

            Guard.ThrowIsNullOrEmpty(this.Value, nameof(this.Value));
        }

        public override string ToString() => this.Value;
    }
}
