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

    internal RoleEfCore(Role role)
        : this()
    {
        Id = role.Id;
        Name = role.Name;
        Description = role.Description;
        IsPublic = role.IsPublic;
        IsDefault = role.IsDefault;
        AppId = role.App.Id;
        TenantId = role.Tenant.Id;
    }
}
