using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

public sealed record class Session
{
    public string Id { get; init; } = Db.NewId();

    public Account Account { get; init; } = null!;

    public string Token { get; set; } = null!;

    public Session()
    {
    }

    public Session(
        Account account,
        string token)
    {
        Account = account;
        Token = token;
    }
}
