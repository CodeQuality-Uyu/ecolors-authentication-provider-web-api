namespace CQ.AuthProvider.WebApi.Controllers.Roles.Models;

public readonly struct RoleBasicInfoResponse
{
    public string Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public string Key { get; init; }

    public bool IsPublic { get; init; }

    public bool IsDefault { get; init; }
}
