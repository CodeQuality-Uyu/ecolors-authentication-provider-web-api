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
    TenantOfAccountBasicInfoResponse Tenant);
