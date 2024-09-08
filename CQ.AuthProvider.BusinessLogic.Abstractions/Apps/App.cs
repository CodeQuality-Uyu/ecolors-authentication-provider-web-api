using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Apps;

public sealed record class App()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; } = null!;

    public Tenant Tenant { get; init; } = null!;

    public bool IsDefault { get; init; }

    public App(
        string name,
        Tenant tenant)
        : this()
    {
        Name = Guard.Normalize(name);
        Tenant = tenant;
    }
}
