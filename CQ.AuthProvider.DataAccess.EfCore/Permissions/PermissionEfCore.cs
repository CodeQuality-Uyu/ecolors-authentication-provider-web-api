using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions;

public sealed record class PermissionEfCore()
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Key { get; init; } = null!;

    public bool IsPublic { get; set; }

    public List<RolePermission> Roles { get; init; } = [];

    public string AppId { get; init; } = null!;

    public AppEfCore App { get; init; } = null!;

    public string TenantId { get; init; } = null!;

    public TenantEfCore Tenant { get; init; } = null!;

    /// <summary>
    /// For new Permission
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="key"></param>
    /// <param name="isPublic"></param>
    /// <param name="app"></param>
    public PermissionEfCore(
        string name,
        string description,
        string key,
        bool isPublic,
        string appId,
        string tenantId)
        : this()
    {
        Name = name;
        Description = description;
        Key = key;
        IsPublic = isPublic;
        AppId = appId;
        TenantId = tenantId;
    }

    internal PermissionEfCore(Permission permission)
        : this(permission.Name,
              permission.Description,
              permission.Key,
              permission.IsPublic,
              permission.App.Id,
              permission.Tenant.Id)
    {
        Id = permission.Id;
    }
}
