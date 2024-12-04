using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.Exceptions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts;

public record class Account()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Email { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public string? ProfilePictureId { get; init; } = null!;

    public string Locale { get; init; } = null!;

    public string TimeZone { get; init; } = null!;

    public List<Role> Roles { get; init; } = [];

    public List<App> Apps { get; init; } = [];

    public Tenant Tenant { get; init; } = null!;

    // For new Account
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
        ProfilePictureId = profilePictureUrl;
        Locale = locale;
        TimeZone = timeZone;
        Roles = [role];
        Tenant = app.Tenant;
        Apps = [app];
    }

    public static Account NewForInvitation(
        string email,
        string firstName,
        string lastName,
        string? profilePictureUrl,
        string locale,
        string timeZone,
        Invitation invitation) => new(
            email,
            firstName,
            lastName,
            profilePictureUrl,
            locale,
            timeZone,
            invitation.Role,
            invitation.App)
        {
            Tenant = invitation.App.Tenant
        };

    public void AssertPermission(string permissionKey)
    {
        var missingPermission = !HasPermission(permissionKey);

        if (missingPermission)
        {
            throw new AccessDeniedException(permissionKey.ToString());
        }
    }

    public bool HasPermission(string permissionKey)
    {
        var hasRole = Roles.Exists(r => r.Id.ToString() == permissionKey);

        return hasRole || CheckPermission(permissionKey);
    }

    private bool CheckPermission(string permissionKey)
    {
        var allPermissions = Roles
           .SelectMany(r => r.Permissions)
        .ToList();

        var hasPermission = allPermissions.Exists(p => p.HasPermissionKey(permissionKey));

        return hasPermission;
    }
}
