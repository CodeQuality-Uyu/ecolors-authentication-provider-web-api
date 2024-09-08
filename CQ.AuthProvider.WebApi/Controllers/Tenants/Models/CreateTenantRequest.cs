using CQ.ApiElements.Dtos;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Tenants.Models;

public sealed record class CreateTenantRequest
    : Request<CreateTenantArgs>
{
    public string? Name { get; init; }

    protected override CreateTenantArgs InnerMap()
    {
        Guard.ThrowIsNullOrEmpty(Name, nameof(Name));

        return new CreateTenantArgs(Name!);
    }
}
