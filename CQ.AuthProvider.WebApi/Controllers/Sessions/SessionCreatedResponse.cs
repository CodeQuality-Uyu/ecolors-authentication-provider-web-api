namespace CQ.AuthProvider.WebApi.Controllers.Sessions;

public sealed record SessionCreatedResponse(
    Guid AccountId,
    string? ProfilePictureId,
    string Email,
    string FirstName,
    string LastName,
    string FullName,
    string Token,
    List<string> Roles,
    List<string> Permissions);
