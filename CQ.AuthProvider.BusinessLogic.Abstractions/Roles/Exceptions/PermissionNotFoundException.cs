using CQ.AuthProvider.BusinessLogic.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Roles.Exceptions;

public class PermissionNotFoundException(List<PermissionKey> permissionKeys) : Exception
{
    public List<PermissionKey> PermissionKeys { get; init; } = permissionKeys;
}
