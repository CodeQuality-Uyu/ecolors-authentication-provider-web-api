namespace CQ.AuthProvider.WebApi.Controllers.Tenants.Models;

public sealed record class CreateTenantRequest
{
    public string? Name { get; init; }
}
