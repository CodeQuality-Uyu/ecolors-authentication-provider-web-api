
namespace CQ.AuthProvider.DataAccess.Mongo.Sessions;

public sealed record class SessionMongo
{
    public string Id { get; init; } = null!;

    public List<string> Tokens { get; init; } = [];

    public string AccountId { get; init; } = null!;

    /// <summary>
    /// For MongoDriver
    /// </summary>
    public SessionMongo()
    {
    }

    public SessionMongo(
        string id,
        string token,
        string accountId)
    {
        Id = id;
        Tokens = [token];
        AccountId = accountId;
    }
}
