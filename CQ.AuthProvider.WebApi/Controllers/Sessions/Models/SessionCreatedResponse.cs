namespace CQ.AuthProvider.WebApi.Controllers.Sessions.Models;

public readonly struct SessionCreatedResponse
{
    public Guid AccountId { get; init; }

    public string? ProfilePictureId { get; init; }

    public string Email { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string FullName { get; init; }

    public string Token { get; init; }

    public List<string> Roles { get; init; }

    public List<string> Permissions { get; init; }
}
