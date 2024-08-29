using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

public readonly struct CreateRoleArgs
{
    public string Name { get; init; }

    public string Description { get; init; }

    public List<string> PermissionKeys { get; init; }

    public bool IsPublic { get; init; }

    public bool IsDefault { get; init; }

    public string? AppId { get; init; }

    public CreateRoleArgs(
        string name,
        string description,
        List<string> permissionKeys,
        bool isPublic,
        bool isDefault,
        string? appId)
    {
        Name = Guard.Normalize(Guard.Encode(name, nameof(name)));
        Guard.ThrowIsMoreThan(Name, 50, nameof(Name));

        Description = Guard.Normalize(Guard.Encode(description, nameof(description)));
        Guard.ThrowIsMoreThan(Description, 200, nameof(Description));

        PermissionKeys = permissionKeys.ConvertAll(p => Guard.Encode(p, nameof(p)));

        var duplicatedKeys = PermissionKeys
            .GroupBy(g => g)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
        if (duplicatedKeys.Count != 0)
        {
            throw new ArgumentException($"The keys must be unique in an app");
        }

        IsPublic = isPublic;
        IsDefault = isDefault;

        if (Guard.IsNotNullOrEmpty(appId))
        {
            Db.ThrowIsInvalidId(appId, nameof(appId));
        }
        AppId = appId;
    }
}
