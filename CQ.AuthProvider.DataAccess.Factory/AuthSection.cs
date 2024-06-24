
namespace CQ.AuthProvider.DataAccess.Factory;

internal sealed record class AuthSection
{
    public const string Name = "Auth";

    public DatabaseEngine Engine { get; init; }
}

internal enum DatabaseEngine
{
    Sql,
    Mongo
}