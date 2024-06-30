
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Accounts;

public sealed record class AccountApp
{
    public string Id { get; init; } = Db.NewId();

    public string AccountId { get; init; } = null!;

    public AccountEfCore Account { get; init; } = null!;

    public string AppId { get; init; } = null!;

    public AppEfCore App { get; init; } = null!;

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    /// <summary>
    /// For EfCore
    /// </summary>
    public AccountApp()
    {
    }

    /// <summary>
    /// For new Account
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="tenantId"></param>
    public AccountApp(
        string appId,
        string tenantId)
    {
        AppId = appId;
        TenantId = tenantId;
    }
}
