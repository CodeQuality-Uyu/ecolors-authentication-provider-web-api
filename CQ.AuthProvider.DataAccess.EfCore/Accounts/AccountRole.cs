
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

public sealed record class AccountRole
{
    public string Id { get; init; } = Db.NewId();

    public string RoleId { get; init; } = null!;

    public RoleEfCore Role { get; init; } = null!;

    public string AccountId { get; init; } = null!;

    public AccountEfCore Account { get; init; } = null!;

    /// <summary>
    /// For EfCore
    /// </summary>
    public AccountRole()
    {
    }

    /// <summary>
    /// When creating account
    /// </summary>
    /// <param name="roleId"></param>
    public AccountRole(string roleId)
    {
        RoleId = roleId;
    }
}
