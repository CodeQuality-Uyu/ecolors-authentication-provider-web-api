namespace CQ.AuthProvider.WebApi.AppConfig;

public sealed record BlobSection
{
    public BlobType Type { get; init; }

    public required BlobConfig Config { get; init; }
}

public enum BlobType
{
    Mock,
    LocalStack,
    Aws
}

public sealed record BlobConfig
{
    public string AccessToken { get; init; } = null!;

    public string SecretToken { get; init; } = null!;

    public string? Region { get; init; }
    
    public string? ServiceUrl { get; init; }
}
