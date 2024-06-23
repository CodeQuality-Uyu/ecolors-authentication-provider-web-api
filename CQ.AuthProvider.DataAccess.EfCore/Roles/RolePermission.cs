using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

public sealed record class RolePermission
{
    public string Id { get; init; } = Db.NewId();

    public string RoleId { get; init; } = null!;

    public RoleEfCore Role { get; init; } = null!;

    public string PermissionId { get; init; } = null!;

    public PermissionEfCore Permission { get; init; } = null!;

    /// <summary>
    /// For Ef Core
    /// </summary>
    public RolePermission()
    {
    }

    /// <summary>
    /// For when creating a new role
    /// </summary>
    /// <param name="permissionId"></param>
    public RolePermission(string permissionId)
    {
        PermissionId = permissionId;
    }

    /// <summary>
    /// For adding new permission to existent role
    /// </summary>
    /// <param name="id"></param>
    /// <param name="permissionId"></param>
    public RolePermission(
        string id,
        string permissionId)
    {
        RoleId = id;
        PermissionId = permissionId;
    }
}
