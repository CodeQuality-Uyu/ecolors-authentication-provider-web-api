namespace CQ.AuthProvider.WebApi.Controllers.Accounts.Models;

public sealed record AccountLoggedResponse(
    string Id,
    string? ProfilePictureId,
    string FullName,
    string FirstName,
    string LastName,
    string Email,
    string Locale,
    string TimeZone,
    List<string> RolesNames,
    List<string> PermissionsKeyes);
