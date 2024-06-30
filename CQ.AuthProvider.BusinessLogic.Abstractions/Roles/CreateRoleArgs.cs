using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

public readonly struct CreateRoleArgs
{
    public string Name { get; init; }

    public string Description { get; init; }

    public RoleKey Key { get; init; }

    public List<PermissionKey> PermissionKeys { get; init; }

    public bool IsPublic { get; init; }

    public bool IsDefault { get; init; }

    public string AppId { get; init; }

    public CreateRoleArgs(
        string name,
        string description,
        string key,
        List<string> permissionKeys,
        bool isPublic,
        bool isDefault,
        string appId)
    {
        Name = Guard.Normalize(Guard.Encode(name, nameof(name)));
        Guard.ThrowIsMoreThan(Name, 50, nameof(Name));

        Description = Guard.Normalize(Guard.Encode(description, nameof(description)));
        Guard.ThrowIsMoreThan(Description, 200, nameof(Description));

        Key = new RoleKey(key);
        PermissionKeys = permissionKeys.ConvertAll(p => new PermissionKey(p));

        IsPublic = isPublic;
        IsDefault = isDefault;

        Db.ThrowIsInvalidId(appId, nameof(appId));
        AppId = appId;
    }
}
