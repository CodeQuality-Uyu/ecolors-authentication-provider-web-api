using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.Mongo.Permissions;

public sealed class PermissionMongo()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string Key { get; init; } = null!;

    public bool IsPublic { get; init; }

    // For new Permission
    public PermissionMongo(
        string id,
        string name,
        string description,
        string key,
        bool isPublic)
        : this()
    {
        Id = id;
        Name = name;
        Description = description;
        Key = key;
        IsPublic = isPublic;
    }
}
