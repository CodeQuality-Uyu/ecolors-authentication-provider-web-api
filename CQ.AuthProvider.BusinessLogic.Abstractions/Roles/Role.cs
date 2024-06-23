using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

public sealed record class Role
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public bool IsPublic { get; init; }

    public RoleKey Key { get; init; } = null!;

    public List<Permission> Permissions { get; init; } = [];

    public bool IsDefault { get; init; }

    public Role()
    {
    }

    public Role(
        string name,
        string description,
        bool isPublic,
        RoleKey key,
        List<Permission> permissions,
        bool isDefault)
    {
        Name = name;
        Description = description;
        IsPublic = isPublic;
        Key = key;
        Permissions = permissions;
        IsDefault = isDefault;
    }
}
