namespace CQ.AuthProvider.BusinessLogic.Accounts;

public record CreateAccountArgs(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Locale,
    string TimeZone,
    string? ProfilePictureId,
    Guid? RoleId,
    Guid TenantId,
    Guid AppId);

public sealed record CreateAccountForArgs(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Locale,
    string TimeZone,
    string? ProfilePictureId,
    Guid? RoleId,
    Guid AppId) : CreateAccountArgs(Email, Password, FirstName, LastName, Locale, TimeZone, ProfilePictureId, RoleId, Guid.Empty, AppId)
{
    public new Guid TenantId { get; set; }
}