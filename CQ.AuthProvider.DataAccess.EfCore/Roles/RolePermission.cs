using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

public sealed record class RolePermission()
{
    public string Id { get; init; } = Db.NewId();

    public string RoleId { get; init; } = null!;

    public RoleEfCore Role { get; init; } = null!;

    public string PermissionId { get; init; } = null!;

    public PermissionEfCore Permission { get; init; } = null!;

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    /// <summary>
    /// For new Role
    /// </summary>
    /// <param name="permissionId"></param>
    /// <param name="tenantId"></param>
    public RolePermission(
        string permissionId,
        string tenantId)
        : this()
    {
        PermissionId = permissionId;
        TenantId = tenantId;
    }

    /// <summary>
    /// For adding permission to role
    /// </summary>
    /// <param name="id"></param>
    /// <param name="permissionId"></param>
    /// <param name="tenantId"></param>
    public RolePermission(
        string id,
        string permissionId,
        string tenantId)
        : this()
    {
        RoleId = id;
        PermissionId = permissionId;
        TenantId = tenantId;
    }
}
