
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

public sealed record class RoleApp()
{
    public string Id { get; init; } = Db.NewId();

    public string RoleId { get; init; } = null!;

    public RoleEfCore Role { get; init; } = null!;

    public string AppId { get; init; } = null!;

    public AppEfCore App { get; init; } = null!;

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    /// <summary>
    /// For new Role
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="tenantId"></param>
    public RoleApp(
        string appId,
        string tenantId)
        : this()
    {
        AppId = appId;
        TenantId = tenantId;
    }
}
