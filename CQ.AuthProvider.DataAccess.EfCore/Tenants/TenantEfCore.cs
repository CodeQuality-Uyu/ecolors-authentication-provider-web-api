
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Tenants;

public sealed record class TenantEfCore()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; } = null!;

    public string OwnerId { get; init; } = null!;

    public AccountEfCore Owner { get; init; } = null!;

    public List<AccountEfCore> Accounts { get; init; } = [];

    /// <summary>
    /// For new Tenant
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public TenantEfCore(
        string id,
        string name)
        : this()
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// For seed data
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ownerId"></param>
    public TenantEfCore(string name)
        : this()
    {
        Name = name;
    }
}
