using CQ.AuthProvider.WebApi.Controllers.Tenants;

namespace CQ.AuthProvider.WebApi.Controllers.Sessions;

public sealed record SessionCreatedResponse(
    Guid Id,
    string? ProfilePictureId,
    string Email,
    string FirstName,
    string LastName,
    string FullName,
    string Token,
    List<string> Roles,
    List<string> Permissions,
    SessionAppLoggedResponse AppLogged,
    TenantOfAccountBasicInfoResponse Tenant);

public sealed record SessionAppLoggedResponse(
    Guid Id,
    string Name);