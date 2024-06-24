
namespace CQ.AuthProvider.DataAccess.Factory;

internal sealed record class AuthSection
{
    public const string Name = "Auth";

    public AuthEngine Engine { get; init; }
}

public enum AuthEngine
{
    Sql,
    Mongo
}