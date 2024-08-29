namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

public sealed record class CreateAccountResult(
    string Id,
    string Email,
    string FullName,
    string FirstName,
    string LastName,
    string? ProfilePictureUrl,
    string Locale,
    string TimeZone,
    string Token,
    List<string> Roles);
