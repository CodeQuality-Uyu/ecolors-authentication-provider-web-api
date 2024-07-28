using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

public sealed record class PermissionKey
{
    #region Permission
    public static readonly PermissionKey GetAllPrivatePermissions = new("getallprivate-permission");
    public static readonly PermissionKey GetAllPermissionsByRoleId = new("getallbyroleid-permission");
    public static readonly PermissionKey GetAllPermissionsOfAppsOfAccountLogged = new("getallofappsofaccountlogged-permission");
    public static readonly PermissionKey GetAllPermissionsOfTenant = new("getalloftenant-permission");
    #endregion

    #region Role
    public static readonly PermissionKey GetAllPrivateRoles = new("getallprivate-role");
    public static readonly PermissionKey GetAllRolesOfAppsOfAccountLogged = new("getallofappsofaccountlogged-role");
    public static readonly PermissionKey GetAllRolesOfTenant = new("getalloftenant-role");
    #endregion

    #region Invitation
    public static readonly PermissionKey GetAllInvitationsOfCreator = new("getallinvitationsofcreator-invitation");
    public static readonly PermissionKey GetAllInvitationsOfTenant = new("getalloftenant-invitation");
    public static readonly PermissionKey GetAllInvitationsOfAppsOfAccountLogged = new("getallofappsofaccountlogged-invitation");
    #endregion

    public static readonly PermissionKey Joker = new("*");

    public static readonly PermissionKey FullAccess = new("full-access");

    private readonly string Value;

    public PermissionKey(string value)
    {
        Value = Guard.Encode(value, nameof(value));
    }

    public override string ToString()
    {
        return Value;
    }
}
