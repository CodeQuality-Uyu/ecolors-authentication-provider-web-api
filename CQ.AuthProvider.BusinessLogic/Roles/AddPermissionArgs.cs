using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Roles;

public readonly struct AddPermissionArgs
{
    public List<string> PermissionsKeys { get; init; }

    public AddPermissionArgs(List<string> permissionsKeys)
    {
        Guard.ThrowIsNullOrEmpty(permissionsKeys, nameof(permissionsKeys));
        PermissionsKeys = permissionsKeys.ConvertAll(p => Guard.Encode(p, nameof(permissionsKeys)));

        var duplicatedKeys = PermissionsKeys
            .GroupBy(g => g)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        if (duplicatedKeys.Count != 0)
        {
            throw new ArgumentException($"Duplicated keys {string.Join(",", duplicatedKeys)}");
        }
    }
}
