using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Apps;

public sealed record class AppEfCore()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = null!;

    public bool IsDefault { get; set; }

    public Guid CoverId { get; init; }

    public CoverBackgroundColorEfCore? BackgroundColor { get; init; }

    public Guid? BackgroundCoverId { get; init; }

    public Guid TenantId { get; init; }

    public TenantEfCore Tenant { get; init; } = null!;

    public Guid? FatherAppId { get; init; }

    public AppEfCore FatherApp { get; init; } = null!;

    public List<RoleEfCore> Roles { get; init; } = [];

    public List<PermissionEfCore> Permissions { get; init; } = [];
}
