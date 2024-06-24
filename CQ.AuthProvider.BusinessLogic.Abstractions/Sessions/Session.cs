using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;

public sealed record class Session(
    Account Account,
    string Token)
{
    public string Id { get; init; } = Db.NewId();
}
