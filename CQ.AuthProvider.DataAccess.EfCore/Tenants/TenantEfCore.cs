using CQ.AuthProvider.DataAccess.EfCore.Accounts;

namespace CQ.AuthProvider.DataAccess.EfCore.Tenants;

public sealed record class TenantEfCore()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Name { get; set; } = null!;

    public Guid MiniLogoId { get; init; }

    public Guid CoverLogoId { get; init; }

    public string? WebUrl { get; set; }

    public Guid OwnerId { get; set; }

    public AccountEfCore Owner { get; set; } = null!;

    public List<AccountEfCore> Accounts { get; init; } = [];
}
