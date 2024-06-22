using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

public readonly struct AddPermissionArgs
{
    public List<PermissionKey> PermissionsKeys { get; init; }

    public AddPermissionArgs(List<string> permissionsKeys)
    {
        Guard.ThrowIsNullOrEmpty(permissionsKeys, nameof(permissionsKeys));
        PermissionsKeys = permissionsKeys.ConvertAll(p => new PermissionKey(p));
    }
}
