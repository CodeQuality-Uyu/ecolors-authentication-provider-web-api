using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions;

public class PermissionEfCore
{
    public string Id { get; set; } = Db.NewId();

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Key { get; set; } = null!;

    public bool IsPublic { get; set; }

    public List<RolePermission> Roles { get; set; } = [];

    public PermissionEfCore()
    {
    }
}
