using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

public sealed record class Session(
    Account Account,
    App App,
    string Token)
{
    public string Id { get; init; } = Db.NewId();
}
