using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

public sealed record class RolePermission()
{
    public string Id { get; init; } = Db.NewId();

    public string RoleId { get; init; } = null!;

    public RoleEfCore Role { get; init; } = null!;

    public string PermissionId { get; init; } = null!;

    public PermissionEfCore Permission { get; init; } = null!;

    // For new Role
    public RolePermission(string permissionId)
        : this()
    {
        PermissionId = permissionId;
    }

    // For adding permission to role
    public RolePermission(
        string id,
        string permissionId)
        : this()
    {
        RoleId = id;
        PermissionId = permissionId;
    }
}
