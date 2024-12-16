namespace CQ.AuthProvider.DataAccess.Mongo.ResetPasswords;

internal sealed class ResetPasswordMongo()
{
    public string Id { get; init; } = null!;

    public string AccountId { get; init; } = null!;

    public string Code { get; init; } = null!;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

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
