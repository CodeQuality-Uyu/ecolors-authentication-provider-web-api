namespace CQ.AuthProvider.BusinessLogic.Accounts;

public sealed record CreateAccountArgs(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Locale,
    string TimeZone,
    string? ProfilePictureKey,
    Guid AppId,
    bool IsPasswordHashed = false);

public sealed record CreateAccountForArgs(
    string Email,
    string FirstName,
    string LastName,
    string Locale,
    string TimeZone,
    string? ProfilePictureKey,
    List<Guid>? AppIds,
    List<Guid> RoleIds);

public sealed record CreateAccountWithTenantArgs(
string Email,
string Password,
string FirstName,
string LastName,
string Locale,
string TimeZone,
string? ProfilePictureKey,
string TenantName);
