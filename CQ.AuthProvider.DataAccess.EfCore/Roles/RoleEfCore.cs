using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

public sealed record class RoleEfCore
{
    public string Id { get; init; } = Db.NewId();

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Key { get; init; } = null!;

    public List<RolePermission> Permissions { get; set; } = [];

    public List<AccountRole> Accounts { get; set; } = [];

    public bool IsPublic { get; set; }

    public bool IsDefault { get; set; }

    public RoleEfCore()
    {
    }

    public RoleEfCore(
        string id,
        string name,
        string description,
        RoleKey key,
        List<Permission> permissions,
        bool isPublic,
        bool isDefault)
        : this()
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
