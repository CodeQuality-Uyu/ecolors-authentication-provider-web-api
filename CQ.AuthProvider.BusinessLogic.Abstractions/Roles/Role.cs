using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

public sealed record class Role()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public bool IsPublic { get; init; }

    public List<Permission> Permissions { get; init; } = [];

    public List<App> Apps { get; init; } = [];

    public Tenant Tenant { get; init; } = null!;

    public bool IsDefault { get; init; }

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
        Apps = [app];
        Tenant = app.Tenant;
    }
}
