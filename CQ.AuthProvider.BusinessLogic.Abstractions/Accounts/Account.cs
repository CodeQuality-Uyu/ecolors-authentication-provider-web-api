using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.Exceptions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

public record class Account(
    string Email,
    string FirstName,
    string LastName,
    string FullName,
    string? ProfilePictureUrl,
    string Locale,
    string TimeZone,
    List<Role> Roles)
{
    public string Id { get; init; } = Db.NewId();

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

        var missingPermission = !allPermissions.Exists(p => p.HasPermission(permission));

        return missingPermission;
    }
}
