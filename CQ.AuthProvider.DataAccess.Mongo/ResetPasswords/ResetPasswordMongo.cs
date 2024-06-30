using CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;
namespace CQ.AuthProvider.DataAccess.Mongo.ResetPasswords;

internal sealed class ResetPasswordMongo
{
    public string Id { get; init; } = null!;

    public string AccountId { get; init; } = null!;

    public string Code { get; init; } = null!;

    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public ResetPasswordStatus Status { get; init; } = ResetPasswordStatus.Pending;

    /// <summary>
    /// For MongoDriver
    /// </summary>
    public ResetPasswordMongo()
    {
    }

    /// <summary>
    /// For new ResetPassword
    /// </summary>
    /// <param name="id"></param>
    /// <param name="code"></param>
    /// <param name="accountId"></param>
    public ResetPasswordMongo(
        string id,
        string code,
        string accountId)
    {
        Id = id;
        AccountId = accountId;
        Code = code;
    }
}
