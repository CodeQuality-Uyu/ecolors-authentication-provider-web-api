namespace CQ.AuthProvider.BusinessLogic.Tenants;

public sealed record CreateTenantArgs(
    string Name,
    Guid? MiniLogoId,
    Guid? CoverLogoId,
    string? WebUrl);
