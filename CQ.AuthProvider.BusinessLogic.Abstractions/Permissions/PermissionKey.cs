using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

public readonly struct PermissionKey
{
    #region Permission
    public const string CanReadPermissionsOfTenant = "can-read-permissions-of-tenant";
    public const string CanReadPrivatePermissions = "can-read-private-permissions";
    #endregion

    #region Role
    public const string CanReadRolesOfTenant = "can-read-roles-of-tenant";
    public const string CanReadPrivateRoles = "can-read-private-roles";
    #endregion

    #region Invitation
    public const string CanReadInvitationsOfTenant = "can-read-invitations-of-tenant";
    #endregion

    public const string Joker = "*";

    public const string FullAccess = "full-access";
}
