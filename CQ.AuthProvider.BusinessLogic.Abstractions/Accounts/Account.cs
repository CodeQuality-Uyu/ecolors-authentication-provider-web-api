using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.Exceptions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

public record class Account()
{
    public string Id { get; init; } = Db.NewId();

    public string Email { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public string? ProfilePictureUrl { get; init; } = null!;

    public string Locale { get; init; } = null!;

    public string TimeZone { get; init; } = null!;

    public List<Role> Roles { get; init; } = [];

    public List<App> Apps { get; init; } = [];

    public Tenant Tenant { get; init; } = null!;

    /// <summary>
    /// For new Account
    /// </summary>
    /// <param name="email"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="profilePictureUrl"></param>
    /// <param name="locale"></param>
    /// <param name="timeZone"></param>
    /// <param name="role"></param>
    /// <param name="tenant"></param>
    public Account(
        string email,
        string firstName,
        string lastName,
        string? profilePictureUrl,
        string locale,
        string timeZone,
        Role role,
        App app)
        : this()
    {
        Email = email;
        FirstName = Guard.Normalize(firstName);
        LastName = Guard.Normalize(lastName);
        FullName = $"{FirstName} {LastName}";
        ProfilePictureUrl = profilePictureUrl;
        Locale = locale;
        TimeZone = timeZone;
        Roles = [role];
        Tenant = app.Tenant;
        Apps = [app];
    }

    public void AssertPermission(PermissionKey permission)
    {
        var missingPermission = !HasPermission(permission);

        if (missingPermission)
        {
            throw new AccessDeniedException(permission.ToString());
        }
    }

    public bool HasPermission(PermissionKey permission)
    {
        var allPermissions = Roles
           .SelectMany(r => r.Permissions)
           .ToList();

        var hasPermission = allPermissions.Exists(p => p.HasPermission(permission));

        return hasPermission;
    }
}
