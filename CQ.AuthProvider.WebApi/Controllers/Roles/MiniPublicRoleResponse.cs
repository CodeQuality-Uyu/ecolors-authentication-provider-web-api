namespace CQ.AuthProvider.WebApi.Controllers.Roles;

public readonly struct MiniPublicRoleResponse
{
    public string Name { get; init; }

    public string Description { get; init; }

    public string Key { get; init; }
}
