namespace CQ.AuthProvider.BusinessLogic.Permissions;

public readonly struct PermissionKey
{
    #region Permission
    public const string CanReadPermissionsOfTenant = "can-read-tenants-permissions";
    public const string CanReadPrivatePermissions = "can-read-private-permissions";
    public const string CanReadPermissionsOfRole = "can-read-permissions-of-role";
    #endregion

    #region Role
    public const string CanReadRolesOfTenant = "can-read-roles-of-tenant";
    public const string CanReadPrivateRoles = "can-read-private-roles";
    #endregion

    public const string Joker = "*";

    public const string FullAccess = "full-access";
}
