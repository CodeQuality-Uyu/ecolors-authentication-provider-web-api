
namespace CQ.AuthProvider.WebApi.Controllers.Permissions.Models;

public sealed record PermissionBasicInfoResponse(
    Guid Id,
    string Name,
    string Description,
    string Key,
    bool IsPublic);
