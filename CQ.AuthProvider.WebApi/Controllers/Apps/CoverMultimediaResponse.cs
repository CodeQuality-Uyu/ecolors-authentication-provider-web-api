using CQ.AuthProvider.WebApi.Controllers.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

public sealed record CoverMultimediaResponse
    : MultimediaResponse
{
    public string? BackgroundColorHex { get; init; }

    public MultimediaResponse? BackgroundCover { get; init; }
}
