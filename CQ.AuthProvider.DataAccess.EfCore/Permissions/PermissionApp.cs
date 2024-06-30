
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions;

public sealed record class PermissionApp
{
    public string Id { get; init; } = null!;

    public string PermissionId { get; init; } = null!;

    public PermissionEfCore Permission { get; init; } = null!;

    public string AppId { get; init; } = null!;

    public AppEfCore App { get; init; } = null!;

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    /// <summary>
    /// For EfCore
    /// </summary>
    public PermissionApp()
    {
    }

    /// <summary>
    /// For new Permission
    /// </summary>
    /// <param name="appId"></param>
    public PermissionApp(string appId)
    {
        AppId = appId;
    }
}
