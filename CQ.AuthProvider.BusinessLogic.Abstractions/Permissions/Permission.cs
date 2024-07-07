using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

public sealed record class Permission()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public bool IsPublic { get; init; }

    public PermissionKey Key { get; init; } = null!;

    public List<App> Apps { get; init; } = []; 

    public Tenant Tenant { get; init; } = null!;

    public Permission(
        string name,
        string description,
        bool isPublic,
        PermissionKey key,
        App app)
        : this()
    {
        Name = name;
        Description = description;
        IsPublic = isPublic;
        Key = key;
        Apps = [app];
        Tenant = app.Tenant;
    }

    public bool HasPermission(PermissionKey permission)
    {
        return Key == PermissionKey.Joker || Key == permission;
    }
}
