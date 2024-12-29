using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Tenants;

public sealed record class Tenant()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = null!;

    public Guid MiniLogoId { get; init; }

    public Guid CoverLogoId { get;init; }

    public string WebUrl { get; set; } = null!;

    public Account Owner { get; init; } = null!;
}
