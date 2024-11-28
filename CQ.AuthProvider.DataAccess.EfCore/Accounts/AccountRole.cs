using CQ.AuthProvider.DataAccess.EfCore.Roles;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

public sealed record class AccountRole()
{
    public required Guid RoleId { get; init; }

    public RoleEfCore Role { get; init; } = null!;

    public required Guid AccountId { get; init; }

    public AccountEfCore Account { get; init; } = null!;
}
