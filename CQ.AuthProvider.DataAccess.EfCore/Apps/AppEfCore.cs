
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Apps;

public sealed record class AppEfCore()
{
    public string Id { get; init; } = null!;

    public string Name { get; init; } = null!;

    public bool IsDefault { get; init; }

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    public List<RoleApp> Roles { get; init; } = [];

    public List<PermissionApp> Permissions { get; init; } = [];

    /// <summary>
    /// For new App
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="tenantId"></param>
    public AppEfCore(
        string id,
        string name,
        string tenantId,
        List<Permission> permissions)
        : this()
    {
        Id = id;
        Name = name;
        TenantId = tenantId;
        Permissions = permissions
            .ConvertAll(p =>
            new PermissionApp(
                p.Id,
                tenantId));
    }
}
