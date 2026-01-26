
namespace CQ.AuthProvider.BusinessLogic.Roles;

public sealed record CreateRoleArgs : CreateBasicRoleArgs
{
    public Guid AppId { get; init; }
}

public sealed record CreateBulkRoleArgs(
    List<CreateBasicRoleArgs> Roles,
    Guid AppId);

public record CreateBasicRoleArgs
{
    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public bool IsPublic { get; init; }

    public bool IsDefault { get; init; }

    public List<string> PermissionKeys { get; init; } = [];
}
