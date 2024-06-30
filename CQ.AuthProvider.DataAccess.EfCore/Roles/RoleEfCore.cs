using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

public sealed record class RoleEfCore
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Key { get; init; } = null!;

    public List<RolePermission> Permissions { get; init; } = [];

    public List<AccountRole> Accounts { get; init; } = [];

    public List<RoleApp> Apps { get; init; } = [];

    public bool IsPublic { get; set; }

    public bool IsDefault { get; set; }

    public string? TenantId { get; init; } = null!;

    public TenantEfCore? Tenant { get; init; } = null!;

    /// <summary>
    /// For EfCore
    /// </summary>
    public RoleEfCore()
    {
    }

    /// <summary>
    /// For new Role
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="key"></param>
    /// <param name="permissions"></param>
    /// <param name="isPublic"></param>
    /// <param name="isDefault"></param>
    public RoleEfCore(
        string id,
        string name,
        string description,
        RoleKey key,
        List<Permission> permissions,
        bool isPublic,
        bool isDefault,
        List<App> apps,
        Tenant tenant)
    {
        Id = id;
        Name = name;
        Description = description;
        Key = key.ToString();
        TenantId = tenant.Id;
        Permissions = permissions.ConvertAll(p => new RolePermission(p.Id, p .Tenant.Id));
        IsPublic = isPublic;
        IsDefault = isDefault;
        Apps = apps.ConvertAll(a => new RoleApp(a.Id, a.Tenant.Id));
    }

    /// <summary>
    /// For seed data
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="key"></param>
    internal RoleEfCore(
        string name,
        string description,
        RoleKey key,
        string tenantId)
    {
        Name = name;
        Description = description;
        Key = key.ToString();
        TenantId = tenantId;
    }
}
