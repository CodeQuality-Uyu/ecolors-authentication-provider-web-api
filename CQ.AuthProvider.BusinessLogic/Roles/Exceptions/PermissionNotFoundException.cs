using CQ.AuthProvider.BusinessLogic.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Roles.Exceptions;

public class PermissionNotFoundException(List<string> permissionKeys) : Exception
{
    public List<string> PermissionKeys { get; init; } = permissionKeys;
}
