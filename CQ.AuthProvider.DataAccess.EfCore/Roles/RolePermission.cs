using CQ.AuthProvider.DataAccess.EfCore.Permissions;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

public sealed record class RolePermission()
{
    public required Guid RoleId { get; init; }

    public RoleEfCore Role { get; init; } = null!;

    public required string PermissionId { get; init; }

    public PermissionEfCore Permission { get; init; } = null!;
}
