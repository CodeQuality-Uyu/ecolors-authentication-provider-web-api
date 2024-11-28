using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Apps;

namespace CQ.AuthProvider.DataAccess.EfCore.Sessions;

public sealed record class SessionEfCore()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Token { get; init; } = null!;

    public Guid AppId { get; init; }

    public AppEfCore App { get; init; } = null!;

    public Guid AccountId { get; init; }

    public AccountEfCore Account { get; init; } = null!;

    // For new Session
    public SessionEfCore(
        Guid appId,
        string token,
        Guid accountId)
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
