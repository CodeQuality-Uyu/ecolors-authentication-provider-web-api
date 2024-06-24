using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

public sealed record class Permission(
    string Name,
    string Description,
    bool IsPublic,
    PermissionKey Key)
{
    public string Id { get; init; } = Db.NewId();

    public bool HasPermission(PermissionKey permission)
    {
        return Key == PermissionKey.Joker || Key == permission;
    }
}
