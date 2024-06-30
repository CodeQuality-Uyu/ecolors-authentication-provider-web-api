namespace CQ.AuthProvider.WebApi.Controllers.Sessions.Models;

public readonly struct CreateSessionResponse
{
    public string AccountId { get; init; }

    public string? ProfilePictureUrl { get; init; }

    public string Email { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string FullName { get; init; }

    public string Token { get; init; }

    public List<string> Roles { get; init; }

    public List<string> Permissions { get; init; }
}
