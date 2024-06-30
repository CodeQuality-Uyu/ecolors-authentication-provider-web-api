
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

public sealed record class AccountRole
{
    public string Id { get; init; } = Db.NewId();

    public string RoleId { get; init; } = null!;

    public RoleEfCore Role { get; init; } = null!;

    public string AccountId { get; init; } = null!;

    public AccountEfCore Account { get; init; } = null!;

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    /// <summary>
    /// For EfCore
    /// </summary>
    public AccountRole()
    {
    }

    /// <summary>
    /// When new Account
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="tenantId"></param>
    public AccountRole(
        string roleId,
        string tenantId)
    {
        RoleId = roleId;
        TenantId = tenantId;
    }

    /// <summary>
    /// For seed data
    /// </summary>
    /// <param name="id"></param>
    /// <param name="roleId"></param>
    /// <param name="tenantId"></param>
    internal AccountRole(
        string id,
        string roleId,
        string tenantId)
    {
        AccountId = id;
        RoleId = roleId;
        TenantId = tenantId;
    }
}
