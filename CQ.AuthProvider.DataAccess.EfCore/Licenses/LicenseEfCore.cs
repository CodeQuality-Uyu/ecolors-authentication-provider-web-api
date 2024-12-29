using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Licenses;

public sealed record LicenseEfCore()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Token { get; init; } = null!;

    public DateTime CreatedAt = DateTime.UtcNow;

    public Guid AppId { get; init; }

    public AppEfCore App { get; init; } = null!;

    public Guid TenantId { get; init; }

    public TenantEfCore Tenant { get; init; } = null!;
}
