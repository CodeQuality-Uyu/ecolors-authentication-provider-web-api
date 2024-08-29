using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

public readonly struct CreatePermissionArgs
{
    public string Name { get; init; }

    public string Description { get; init; }

    public string Key { get; init; }

    public bool IsPublic { get; init; }

    public string? AppId { get; init; }

    public CreatePermissionArgs(
        string name,
        string description,
        string key,
        bool isPublic,
        string? appId)
    {
        Name = Guard.Normalize(Guard.Encode(name.Trim(), nameof(name)));
        Guard.ThrowIsMoreThan(Name, 50, nameof(Name));

        Description = Guard.Normalize(Guard.Encode(description.Trim(), nameof(description)));
        Guard.ThrowIsMoreThan(Description, 200, nameof(Description));

        Key = Guard.Encode(key, nameof(key));
        IsPublic = isPublic;

        if (Guard.IsNotNullOrEmpty(appId))
        {
            Db.ThrowIsInvalidId(appId, nameof(appId));
        }
        AppId = appId;
    }
}
