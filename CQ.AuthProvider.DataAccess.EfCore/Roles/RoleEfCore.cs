using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

public sealed record class RoleEfCore()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Key { get; init; } = null!;

    public bool IsPublic { get; set; }

    public bool IsDefault { get; set; }

    public List<RolePermission> Permissions { get; init; } = [];

    public List<AccountRole> Accounts { get; init; } = [];

    public List<RoleApp> Apps { get; init; } = [];

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

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
        List<App> apps)
        : this()
    {
        Id = id;
        Name = name;
        Description = description;
        Key = key.ToString();
        IsPublic = isPublic;
        IsDefault = isDefault;
        Permissions = permissions
            .ConvertAll(p =>
            new RolePermission(
                p.Id,
                p.Tenant.Id));
        Apps = apps
            .ConvertAll(a =>
            new RoleApp(
                a.Id,
                a.Tenant.Id));
        var tenant = apps.First().Tenant;
        TenantId = tenant.Id;
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
        : this()
    {
        Name = name;
        Description = description;
        Key = key.ToString();
        TenantId = tenantId;
    }
}
