using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions;

public sealed record class PermissionEfCore()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Key { get; init; } = null!;

    public bool IsPublic { get; set; }

    public List<RoleEfCore> Roles { get; init; } = [];

    public Guid AppId { get; init; }

    public AppEfCore App { get; init; } = null!;

    public Guid TenantId { get; init; }

    public TenantEfCore Tenant { get; init; } = null!;

    internal PermissionEfCore(Permission permission)
        : this()
    {
        Id = permission.Id;
        Name = permission.Name;
        Description = permission.Description;
        Key = permission.Key;
        IsPublic = permission.IsPublic;
        AppId = permission.App.Id;
        TenantId = permission.Tenant.Id;
    }
}
