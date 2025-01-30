namespace CQ.AuthProvider.WebApi.AppConfig;

public sealed record BlobConfiguration
{
    public FakeBlobConfiguration Fake { get; init; } = null!;

    public string AccessToken { get; init; } = null!;

    public string SecretToken { get; init; } = null!;

    public string? Region { get; init; }
}

public sealed record FakeBlobConfiguration
{
    public bool IsActive { get; init; }

    public string ServiceUrl { get; init; } = null!;
}
