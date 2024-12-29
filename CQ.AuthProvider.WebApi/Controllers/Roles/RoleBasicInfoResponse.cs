namespace CQ.AuthProvider.WebApi.Controllers.Roles;

public sealed record RoleBasicInfoResponse(
    Guid Id,
    string Name,
    string Description,
    bool IsPublic,
    bool IsDefault);
