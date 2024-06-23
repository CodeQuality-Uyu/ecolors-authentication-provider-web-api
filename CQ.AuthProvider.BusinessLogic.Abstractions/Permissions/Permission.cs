using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

public sealed record class Permission
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public bool IsPublic { get; init; }

    public PermissionKey Key { get; init; } = null!;

    public Permission()
    {
    }

    public Permission(
        string name,
        string description,
        bool isPublic,
        PermissionKey key)
    {
        Name = name;
        Description = description;
        IsPublic = isPublic;
        Key = key;
    }
}
