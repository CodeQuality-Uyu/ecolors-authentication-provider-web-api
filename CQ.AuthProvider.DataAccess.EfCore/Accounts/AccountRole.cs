using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

public sealed record class AccountRole()
{
    public string Id { get; init; } = Db.NewId();

    public string RoleId { get; init; } = null!;

    public RoleEfCore Role { get; init; } = null!;

    public string AccountId { get; init; } = null!;

    public AccountEfCore Account { get; init; } = null!;

    // When new Account
    public AccountRole(
        string accountId,
        string roleId)
        : this()
    {
        AccountId = accountId;
        RoleId = roleId;
    }
}
