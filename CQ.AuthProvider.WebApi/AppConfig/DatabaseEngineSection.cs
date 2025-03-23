namespace CQ.AuthProvider.WebApi.AppConfig;

public sealed record DatabaseEngineSection
{
    public const string SectionName = "DatabaseEngine";

    public required string Auth { get; init; }

    public required string Identity { get; init; }
}
