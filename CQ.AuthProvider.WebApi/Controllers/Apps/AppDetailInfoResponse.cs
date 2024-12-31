using CQ.AuthProvider.WebApi.Controllers.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

public readonly struct AppDetailInfoResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public MultimediaResponse CoverMultimedia { get; init; }
}