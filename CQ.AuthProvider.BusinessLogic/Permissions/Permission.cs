using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Permissions;

public sealed record class Permission()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public bool IsPublic { get; init; }

    public string Key { get; init; } = null!;

    public App App { get; init; } = null!;

    public Tenant Tenant { get; init; } = null!;

    public Permission(
        string name,
        string description,
        bool isPublic,
        string key,
        App app)
        : this()
    {
        Name = Guard.Normalize(name);
        Description = Guard.Normalize(description);
        IsPublic = isPublic;
        Key = key;
        App = app;
        Tenant = app.Tenant;
    }

    public bool HasPermissionKey(string permissionKey)
    {
        return Key == permissionKey;
    }
}
