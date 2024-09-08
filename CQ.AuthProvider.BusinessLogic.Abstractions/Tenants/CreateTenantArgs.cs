using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Tenants;

public readonly struct CreateTenantArgs(string name)
{
    public string Name { get; } = Guard.Encode(name, nameof(name));
}
