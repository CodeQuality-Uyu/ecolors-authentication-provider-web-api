namespace CQ.AuthProvider.WebApi.Controllers.Models;

public sealed record MultimediaResponse(
    Guid Id,
    string ReadUrl,
    string WriteUrl);
