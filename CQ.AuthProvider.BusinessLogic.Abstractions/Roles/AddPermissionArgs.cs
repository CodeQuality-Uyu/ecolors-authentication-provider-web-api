using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

public readonly struct AddPermissionArgs
{
    public List<string> PermissionsKeys { get; init; }

    public AddPermissionArgs(List<string> permissionsKeys)
    {
        Guard.ThrowIsNullOrEmpty(permissionsKeys, nameof(permissionsKeys));
        PermissionsKeys = permissionsKeys.ConvertAll(p => Guard.Encode(p, nameof(permissionsKeys)));
    }
}
