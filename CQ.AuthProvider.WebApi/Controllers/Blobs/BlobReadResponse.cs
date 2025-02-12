namespace CQ.AuthProvider.WebApi.Controllers.Blobs;

public record BlobReadResponse
{
    public Guid Id { get; init; }

    public string Key { get; init; } = null!;

    public string ReadUrl { get; init; } = null!;
}
