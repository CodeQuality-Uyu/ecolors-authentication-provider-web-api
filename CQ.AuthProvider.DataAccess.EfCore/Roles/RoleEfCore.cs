using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

public sealed record class RoleEfCore()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsPublic { get; set; }

    public bool IsDefault { get; set; }

    public List<PermissionEfCore> Permissions { get; init; } = [];

    public Guid AppId { get; init; }

    public AppEfCore App { get; init; } = null!;

    public Guid TenantId { get; init; }

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
