using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.Exceptions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

public sealed record class Account
{
    public string Id { get; init; } = Db.NewId();

    public string Email { get; init; } = null!;

    public string? ProfilePictureUrl { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Locale { get; init; } = null!;

    public string TimeZone { get; init; } = null!;

    public List<Role> Roles { get; init; } = null!;

    public Account()
    {
    }

    public Account(
        string email,
        string firstName,
        string lastName,
        string fullName,
        string? profilePictureUrl,
        string locale,
        string timeZone,
        List<Role> roles)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        FullName = fullName;
        ProfilePictureUrl = profilePictureUrl;
        Roles = roles;
        Locale = locale;
        TimeZone = timeZone;
    }

    public void AssertPermission(PermissionKey permission)
    {
        var hasPermission = Permissions.Exists(p => p == permission || p == PermissionKey.Joker);

        if (!hasPermission)
        {
            throw new AccessDeniedException(permission.ToString());
        }
    }

    public bool HasPermission(PermissionKey permission)
    {
        return Permissions.Contains(permission);
    }
}
