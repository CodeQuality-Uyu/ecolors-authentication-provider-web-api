namespace CQ.AuthProvider.BusinessLogic.Permissions;

public sealed record CreatePermissionArgs : CreateBasicPermissionArgs
{
    public Guid AppId { get; init; }
}

public sealed record CreateBulkPermissionArgs(
    List<CreateBasicPermissionArgs> Permissions,
    Guid AppId);

public record CreateBasicPermissionArgs
{
    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string Key { get; init; } = null!;

    public bool IsPublic { get; init; }
}
