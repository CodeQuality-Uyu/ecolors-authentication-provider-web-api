namespace CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;

public sealed record class Permission
{
    public string Id { get; init; } = null!;

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public bool IsPublic { get; init; }

    public PermissionKey Key { get; init; } = null!;
}
