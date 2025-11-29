namespace CQ.AuthProvider.BusinessLogic.Blobs;

public record BlobReadResponse
{
    public string Key { get; init; } = null!;

    public string Url { get; init; } = null!;
}
