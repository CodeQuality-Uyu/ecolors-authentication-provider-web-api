using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

public sealed record class RoleEfCore
{
    public string Id { get; init; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Key { get; init; } = null!;

    public List<RolePermission> Permissions { get; set; } = [];

    public List<AccountRole> Accounts { get; set; } = [];

    public bool IsPublic { get; set; }

    public bool IsDefault { get; set; }

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
        bool isDefault)
    {
        Id = id;
        Name = name;
        Description = description;
        Key = key.ToString();
        Permissions = permissions.ConvertAll(p => new RolePermission(p.Id));
        IsPublic = isPublic;
        IsDefault = isDefault;
    }
}
