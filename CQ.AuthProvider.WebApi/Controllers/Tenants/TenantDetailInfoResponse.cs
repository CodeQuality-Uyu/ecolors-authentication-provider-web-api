namespace CQ.AuthProvider.WebApi.Controllers.Tenants;

public sealed record TenantDetailInfoResponse(
    Guid Id,
    string Name,
    OwnerTenantBasicInfoResponse Owner);
