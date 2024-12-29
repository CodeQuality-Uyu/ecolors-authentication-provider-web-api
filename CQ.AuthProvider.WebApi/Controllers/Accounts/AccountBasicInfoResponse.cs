namespace CQ.AuthProvider.WebApi.Controllers.Accounts;

public sealed record AccountBasicInfoResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string? ProfilePictureId);
