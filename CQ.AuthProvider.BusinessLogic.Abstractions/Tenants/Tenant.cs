

using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;

public sealed record class Tenant
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; }
}
