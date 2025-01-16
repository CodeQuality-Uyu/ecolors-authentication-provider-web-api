using CQ.AuthProvider.WebApi.Controllers.Apps;

namespace CQ.AuthProvider.WebApi.Controllers.Roles;

public sealed record RoleBasicInfoResponse(
    Guid Id,
    string Name,
    string Description,
    bool IsPublic,
    bool IsDefault,
    RoleAppBasicInfoResponse App);

public sealed record RoleAppBasicInfoResponse(
    Guid Id,
    string Name);
