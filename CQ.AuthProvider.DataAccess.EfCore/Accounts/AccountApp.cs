using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

public sealed record class AccountApp()
{
    public string Id { get; init; } = Db.NewId();

    public string AccountId { get; init; } = null!;

    public AccountEfCore Account { get; init; } = null!;

    public string AppId { get; init; } = null!;

    public AppEfCore App { get; init; } = null!;

    //For new Account
    public AccountApp(string appId)
        : this()
    {
        AppId = appId;
    }
}
