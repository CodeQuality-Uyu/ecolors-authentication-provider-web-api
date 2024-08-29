
namespace CQ.AuthProvider.WebApi.Controllers.Permissions.Models;

public readonly struct PermissionBasicInfoResponse
{
    public string Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public string Key { get; init; }

    public bool IsPublic { get; init; }
}
