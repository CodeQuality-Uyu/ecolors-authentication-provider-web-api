namespace CQ.AuthProvider.WebApi.AppConfig;

internal sealed record VersionSection
{
    public string Version { get; init; } = null!;

    public string BuildDate { get; init; } = null!;

    public string PrLink { get; init; } = null!;
}
