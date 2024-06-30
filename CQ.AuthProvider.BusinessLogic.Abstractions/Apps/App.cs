
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Apps;

public sealed record class App()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; } = null!;

    public Tenant Tenant { get; init; } = null!;

    public App(
        string name,
        Tenant tenant)
        : this()
    {
        Name = Guard.Normalize(name);
        Tenant = tenant;
    }
}
