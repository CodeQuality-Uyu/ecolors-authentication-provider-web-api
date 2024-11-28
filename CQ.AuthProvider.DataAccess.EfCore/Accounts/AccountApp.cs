using CQ.AuthProvider.DataAccess.EfCore.Apps;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

public sealed record class AccountApp()
{
    public required Guid AccountId { get; init; }

    public AccountEfCore Account { get; init; } = null!;

    public required Guid AppId { get; init; }

    public AppEfCore App { get; init; } = null!;
}
