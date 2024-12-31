using CQ.AuthProvider.WebApi.Controllers.Models;

namespace CQ.AuthProvider.WebApi.Controllers.Apps;

public readonly struct AppDetailInfoResponse
{
    public Guid Id { get; }
    
    public string Name { get; }

    public MultimediaResponse CoverMultimedia { get; }
}