using CQ.AuthProvider.WebApi.Controllers.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

public sealed record AppDetailInfoResponse(
    Guid Id,
    string Name,
    CoverMultimediaResponse CoverMultimedia);
