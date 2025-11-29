using CQ.AuthProvider.BusinessLogic.Blobs;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

public sealed record CoverBlobResponse
    : BlobReadResponse
{
    public CoverBackgroundColorResponse? BackgroundColor { get; init; }

    public BlobReadResponse? BackgroundCover { get; init; }
}

public sealed record CoverBackgroundColorResponse
{
    public List<string> Colors { get; init; } = [];

    public string Config { get; init; } = null!;
}