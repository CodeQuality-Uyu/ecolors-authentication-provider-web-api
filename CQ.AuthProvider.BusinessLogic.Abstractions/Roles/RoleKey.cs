using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

public sealed record class RoleKey
{
    public static readonly RoleKey Admin = new("admin");

    public static readonly RoleKey TenantOwner = new("tenant-owner");

    private readonly string Value;

    public RoleKey(string value)
    {
        Value = Guard.Encode(value, nameof(value));
    }

    public override string ToString()
    {
        return Value;
    }
}
