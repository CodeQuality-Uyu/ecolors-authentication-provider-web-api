
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Sessions;

public sealed record class SessionEfCore
{
    public string Id { get; init; } = null!;

    public string Token { get; init; } = null!;

    public string AccountId { get; init; } = null!;

    public AccountEfCore Account { get; init; } = null!;

    /// <summary>
    /// For EfCore
    /// </summary>
    public SessionEfCore()
    {
    }


    /// <summary>
    /// For new Session
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token"></param>
    /// <param name="accountId"></param>
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
