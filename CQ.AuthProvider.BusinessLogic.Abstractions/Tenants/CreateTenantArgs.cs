using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;

public readonly struct CreateTenantArgs(string name)
{
    public string Name { get; } = Guard.Encode(name, nameof(name));
}
