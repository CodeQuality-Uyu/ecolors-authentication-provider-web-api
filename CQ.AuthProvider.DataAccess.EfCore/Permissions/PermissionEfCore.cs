using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions;

public class PermissionEfCore
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Key { get; set; } = null!;

    public bool IsPublic { get; set; }

    public List<RolePermission> Roles { get; set; } = [];

    /// <summary>
    /// For EfCore
    /// </summary>
    public PermissionEfCore()
    {
    }

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
        bool isPublic)
    {
        Id = id;
        Name = name;
        Description = description;
        Key = key.ToString();
        IsPublic = isPublic;
    }
}
