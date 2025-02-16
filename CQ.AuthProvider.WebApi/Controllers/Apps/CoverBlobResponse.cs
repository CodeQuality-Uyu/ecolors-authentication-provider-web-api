using CQ.AuthProvider.WebApi.Controllers.Blobs;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

public sealed record CoverBlobResponse
    : BlobReadResponse
{
    public string? BackgroundColorHex { get; init; }

    public BlobReadResponse? BackgroundCover { get; init; }
}
