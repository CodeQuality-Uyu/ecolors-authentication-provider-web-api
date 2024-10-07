using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Tenants;

public sealed record class TenantEfCore()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; set; } = null!;

    public string OwnerId { get; set; } = null!;

    public AccountEfCore Owner { get; set; } = null!;

    public List<AccountEfCore> Accounts { get; init; } = [];

    // For new Tenant
    public TenantEfCore(
        string id,
        string name)
        : this()
    {
        Id = id;
        Name = name;
    }
}
