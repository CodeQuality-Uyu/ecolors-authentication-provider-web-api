using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Sessions;

public sealed record class SessionEfCore()
{
    public string Id { get; init; } = Db.NewId();

    public string Token { get; init; } = null!;

    public string AppId { get; init; } = null!;

    public AppEfCore App { get; init; } = null!;

    public string AccountId { get; init; } = null!;

    public AccountEfCore Account { get; init; } = null!;

    // For new Session
    public SessionEfCore(
        string appId,
        string token,
        string accountId)
        : this()
    {
        AppId = appId;
        Token = token;
        AccountId = accountId;
    }

    internal SessionEfCore(Session session)
        : this(session.App.Id,
              session.Token,
              session.Account.Id)
    {
        Id = session.Id;
    }
}
