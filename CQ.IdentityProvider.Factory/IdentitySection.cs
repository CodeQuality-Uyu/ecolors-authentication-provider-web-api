

namespace CQ.IdentityProvider.Factory;

internal sealed record class IdentitySection
{
    public const string Name = "Identity";

    public IdentityEngine Engine { get; init; }
}

internal enum IdentityEngine
{
    Sql,
    Mongo,
    Firebase
}
