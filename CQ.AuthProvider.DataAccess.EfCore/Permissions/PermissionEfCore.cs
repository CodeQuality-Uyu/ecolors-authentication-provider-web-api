using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions;

public sealed record class PermissionEfCore()
{
    public string Id { get; set; } = Db.NewId();

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Key { get; set; } = null!;

    public bool IsPublic { get; set; }

    public List<RolePermission> Roles { get; init; } = [];

    public List<PermissionApp> Apps { get; init; } = [];

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    /// <summary>
    /// For new Permission
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="key"></param>
    /// <param name="isPublic"></param>
    public PermissionEfCore(
        string id,
        string name,
        string description,
        PermissionKey key,
        bool isPublic,
        List<App> apps)
        : this()
    {
        Id = id;
        Name = name;
        Description = description;
        Key = key.ToString();
        IsPublic = isPublic;
        Apps = apps.ConvertAll(a => new PermissionApp(a.Id, a.Tenant.Id));
        var tenant = apps.First().Tenant;
        TenantId = tenant.Id;
    }

    /// <summary>
    /// For seed data
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="key"></param>
    internal PermissionEfCore(
        string name,
        string description,
        PermissionKey key,
        string tenantId)
        : this()
    {
        Name = name;
        Description = description;
        Key = key.ToString();
        TenantId = tenantId;
    }
}
