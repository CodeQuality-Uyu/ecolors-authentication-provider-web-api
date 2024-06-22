using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

public sealed record class Session
{
    public string Id { get; init; } = Db.NewId();

    public string AccountId { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string Token { get; set; } = Db.NewId();

    public Session()
    {
    }

    public Session(
        string accountId,
        string email,
        string? token = null)
        : this()
    {
        AccountId = accountId;
        Email = email;
        Token = token ?? Token;
    }
}
