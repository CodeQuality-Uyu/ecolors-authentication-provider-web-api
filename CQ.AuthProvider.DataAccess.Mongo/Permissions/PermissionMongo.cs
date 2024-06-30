using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.Mongo.Permissions;

public sealed class PermissionMongo
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string Key { get; init; } = null!;

    public bool IsPublic { get; init; }

    /// <summary>
    /// For MongoDriver
    /// </summary>
    public PermissionMongo()
    {
    }

    /// <summary>
    /// For new Permission
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="key"></param>
    /// <param name="isPublic"></param>
    public PermissionMongo(
        string id,
        string name,
        string description,
        PermissionKey key,
        bool isPublic)
    {
        Id = id;
        Name = name;
        Description = description;
        Key = key.ToString();
        IsPublic = isPublic;
    }
}
