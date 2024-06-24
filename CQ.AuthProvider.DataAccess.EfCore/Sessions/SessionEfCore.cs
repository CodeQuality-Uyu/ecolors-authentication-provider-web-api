
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Sessions;

internal sealed record class SessionEfCore
{
    public string Id { get; init; } = Db.NewId();

    public string Token { get; init; } = null!;

    public string AccountId { get; init; } = null!;

    public AccountEfCore Account { get; init; } = null!;

    public SessionEfCore()
    {
    }

    public SessionEfCore(
        string id,
        string token,
        string accountId)
    {
        Id = id;
        Token = token;
        AccountId = accountId;
    }
}
