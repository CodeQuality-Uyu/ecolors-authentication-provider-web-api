namespace CQ.AuthProvider.BusinessLogic.AppConfig;

public sealed record BlobSection
{
    public BlobType Type { get; init; }

    public string BucketName { get; init; } = null!;

    public string TemporaryObject { get; init; } = null!;
}

public enum BlobType
{
    Mock,
    LocalStack,
    Aws
}
