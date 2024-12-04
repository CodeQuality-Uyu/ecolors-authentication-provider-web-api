using AutoMapper;
using CQ.ApiElements;
using CQ.ApiElements.Filters.Authentications;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.WebApi.Controllers.Tenants.Models;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.UnitOfWork.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Tenants;

[ApiController]
[Route("tenants")]
[BearerAuthentication]
[SecureAuthorization(ContextItem.AccountLogged)]
public sealed class TenantController(
    ITenantService tenantService,
    IMapper mapper)
    : ControllerBase
{
    [HttpPost]
    public async Task CreateAsync(CreateTenantRequest request)
    {
        var args = request.Map();

        var accountLogged = this.GetAccountLogged();

        await tenantService
            .CreateAsync(
            args,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<Pagination<TenantBasicInfoResponse>> GetAllAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var tenants = await tenantService
            .GetAllAsync(
            page,
            pageSize)
            .ConfigureAwait(false);

        return mapper.Map<Pagination<TenantBasicInfoResponse>>(tenants);
    }

    [HttpGet("{id}")]
    public async Task<TenantDetailInfoResponse> GetByIdAsync(Guid id)
    {
        var accountLogged = this.GetAccountLogged();

        var tenant = await tenantService
            .GetByIdAsync(
            id,
            accountLogged)
            .ConfigureAwait(false);

        return mapper.Map<TenantDetailInfoResponse>(tenant);
    }
}
