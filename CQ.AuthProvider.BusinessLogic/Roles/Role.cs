using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Roles;

public sealed record class Role()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public bool IsPublic { get; init; }

    public List<Permission> Permissions { get; init; } = [];

    public App App { get; init; } = null!;

    public string AppId => App.Id;

    public Tenant Tenant { get; init; } = null!;

    public bool IsDefault { get; init; }

    // For new Role
    public Role(
        string name,
        string description,
        bool isPublic,
        List<Permission> permissions,
        bool isDefault,
        App app)
        : this()
    {
        Name = name;
        Description = description;
        IsPublic = isPublic;
        Permissions = permissions;
        IsDefault = isDefault;
        App = app;
        Tenant = app.Tenant;
    }
}
