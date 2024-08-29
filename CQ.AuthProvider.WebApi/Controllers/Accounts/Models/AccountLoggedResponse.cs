using CQ.ApiElements.Dtos;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts.Models;

public record class AccountLoggedResponse
{
    public string Id { get; init; } = null!;

    public string? ProfilePictureUrl { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string Locale { get; init; } = null!;

    public string TimeZone { get; init; } = null!;

    public List<string> Roles { get; init; } = null!;

    public List<string> Permissions { get; init; } = null!;
}
