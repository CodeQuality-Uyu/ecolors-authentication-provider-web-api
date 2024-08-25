namespace CQ.AuthProvider.DataAccess.Mongo.ResetPasswords;

internal sealed class ResetPasswordMongo()
{
    public string Id { get; init; } = null!;

    public string AccountId { get; init; } = null!;

    public string Code { get; init; } = null!;

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public ResetPasswordMongo(
        string id,
        string code,
        string accountId)
        : this()
    {
        Id = id;
        AccountId = accountId;
        Code = code;
    }
}
