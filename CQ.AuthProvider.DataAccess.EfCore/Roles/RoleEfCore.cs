using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

public sealed record class RoleEfCore()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsPublic { get; set; }

    public bool IsDefault { get; set; }

    public List<PermissionEfCore> Permissions { get; init; } = [];

    public string AppId { get; init; } = null!;

    public AppEfCore App { get; init; } = null!;

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    // For new Role
    public RoleEfCore(
        string name,
        string description,
        List<PermissionEfCore> permissions,
        bool isPublic,
        bool isDefault,
        App app)
        : this()
    {
        Name = name;
        Description = description;
        IsPublic = isPublic;
        IsDefault = isDefault;
        Permissions = permissions;
        AppId = app.Id;
        TenantId = app.Tenant.Id;
    }

    internal RoleEfCore(Role role)
        : this(role.Name,
              role.Description,
              [],
              role.IsPublic,
              role.IsDefault,
              role.App)
    {
        Id = role.Id;
    }
}
