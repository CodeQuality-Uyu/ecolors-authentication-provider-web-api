namespace CQ.AuthProvider.BusinessLogic.AppConfig;

public sealed record BlobSection
{
    public BlobType Type { get; init; }

    public BlobConfig Config { get; init; } = null!;

    public string BucketName { get; init; } = null!;

    public string TemporaryObject { get; init; } = null!;
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
