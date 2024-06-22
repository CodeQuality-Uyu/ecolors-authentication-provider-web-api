using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

public readonly struct CreatePermissionArgs
{
    public readonly string Name { get; init; }

    public readonly string Description { get; init; }

    public readonly PermissionKey Key { get; init; }

    public readonly bool IsPublic { get; init; }

    public CreatePermissionArgs(
        string name,
        string description,
        string key,
        bool isPublic)
    {
        Name = Guard.Normalize(Guard.Encode(name.Trim(), nameof(name)));
        Guard.ThrowIsMoreThan(Name, 50, nameof(Name));

        Description = Guard.Normalize(Guard.Encode(description.Trim(), nameof(description)));
        Guard.ThrowIsMoreThan(Description, 200, nameof(Description));

        Key = new PermissionKey(key);
        IsPublic = isPublic;
    }
}
