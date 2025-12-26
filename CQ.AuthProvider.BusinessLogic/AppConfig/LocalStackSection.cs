namespace CQ.AuthProvider.BusinessLogic.AppConfig;

public sealed record LocalStackSection
{
    public string AccessToken { get; init; } = null!;

    public string SecretToken { get; init; } = null!;

    public string? ServiceUrl { get; init; }
}