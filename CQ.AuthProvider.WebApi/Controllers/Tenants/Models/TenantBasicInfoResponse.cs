namespace CQ.AuthProvider.WebApi.Controllers.Tenants.Models;

public sealed record TenantBasicInfoResponse(
    string Id,
    string Name,
    OwnerTenantBasicInfoResponse Owner);

public sealed record OwnerTenantBasicInfoResponse(
    string Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email);