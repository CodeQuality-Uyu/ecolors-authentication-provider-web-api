using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Tenants;

namespace CQ.AuthProvider.BusinessLogic.Licenses;

public sealed record class License()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Token { get; set; } = Guid.NewGuid().ToString();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public App App { get; init; } = null!;

    public Tenant Tenant { get; init; } = null!;

    public License(App app)
        : this()
    {
        App = app;
        Tenant = app.Tenant;
    }
}
