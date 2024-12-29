using AutoMapper;
using CQ.ApiElements.Filters.Authentications;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.UnitOfWork.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Tenants;

[ApiController]
[Route("tenants")]
[BearerAuthentication]
[SecureAuthorization]
public sealed class TenantController(
    ITenantService _tenantService,
    [FromKeyedServices(MapperKeyedService.Presentation)] IMapper _mapper)
    : ControllerBase
{
    [HttpPost]
    public async Task CreateAsync(CreateTenantArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        await _tenantService
            .CreateAsync(
            request,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<Pagination<TenantBasicInfoResponse>> GetAllAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var tenants = await _tenantService
            .GetAllAsync(
            page,
            pageSize)
            .ConfigureAwait(false);

        return _mapper.Map<Pagination<TenantBasicInfoResponse>>(tenants);
    }

    [HttpGet("{id}")]
    public async Task<TenantDetailInfoResponse> GetByIdAsync(Guid id)
    {
        var accountLogged = this.GetAccountLogged();

        var tenant = await _tenantService
            .GetByIdAsync(
            id,
            accountLogged)
            .ConfigureAwait(false);

        return _mapper.Map<TenantDetailInfoResponse>(tenant);
    }
}
