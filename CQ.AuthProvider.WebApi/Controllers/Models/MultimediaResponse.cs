namespace CQ.AuthProvider.WebApi.Controllers.Models;

public record MultimediaResponse
{
    public Guid Id { get; init; }

    public string ReadUrl { get; init; } = null!;

    public string WriteUrl { get; init; } = null!;
}
