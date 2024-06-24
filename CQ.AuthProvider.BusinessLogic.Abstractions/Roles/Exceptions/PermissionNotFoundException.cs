using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles.Exceptions;

public class PermissionNotFoundException(List<PermissionKey> permissionKeys) : Exception
{
    public List<PermissionKey> PermissionKeys { get; init; } = permissionKeys;
}
