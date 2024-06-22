using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Roles;

public sealed record class Role
{
    public string Id { get; init; } = null!;

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public bool IsPublic { get; init; }

    public RoleKey Key { get; init; } = null!;

    public List<PermissionKey> Permissions { get; init; } = null!;

    public bool IsDefault { get; init; }
}
