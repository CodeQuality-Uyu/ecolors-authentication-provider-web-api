namespace CQ.AuthProvider.WebApi.Controllers.Permissions;

public sealed record PermissionBasicInfoResponse(
    Guid Id,
    string Name,
    string Description,
    string Key,
    bool IsPublic);
