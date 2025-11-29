namespace CQ.AuthProvider.BusinessLogic.Tenants;

public sealed record class Tenant()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = null!;

    public string? MiniLogoKey { get; init; }

    public string? CoverLogoKey { get; init; }

    public string? WebUrl { get; set; }
}
