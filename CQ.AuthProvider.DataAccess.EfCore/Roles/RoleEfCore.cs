using CQ.AuthProvider.DataAccess.EfCore.Accounts;
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
}
