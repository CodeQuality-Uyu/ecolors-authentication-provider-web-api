namespace CQ.AuthProvider.WebApi.Controllers.Accounts.Models;

public sealed record AccountBasicInfoResponse(
    string Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string? ProfilePictureId);
