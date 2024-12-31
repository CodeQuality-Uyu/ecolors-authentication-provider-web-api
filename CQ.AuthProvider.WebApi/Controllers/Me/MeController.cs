using AutoMapper;
using CQ.ApiElements.Filters.Authentications;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Sessions;
using CQ.AuthProvider.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Me;

[ApiController]
[Route("me")]
[BearerAuthentication]
public sealed class MeController(
    IAccountService _accountService,
    ITenantService _tenantService,
    [FromKeyedServices(MapperKeyedService.Presentation)] IMapper _mapper)
    : ControllerBase
{
    [HttpGet]
    public SessionCreatedResponse GetMeAsync()
    {
        var accountLogged = this.GetAccountLogged();

        return _mapper.Map<SessionCreatedResponse>(accountLogged);
    }

    [HttpPatch("me/password")]
    public async Task UpdatePasswordAsync(UpdatePasswordArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        await _accountService
            .UpdatePasswordAsync(request, accountLogged)
            .ConfigureAwait(false);
    }


    [HttpPatch("tenants/owner")]
    [SecureAuthorization]
    public async Task TransferTenantAsync(TransferTenantRequest request)
    {
        var accountLogged = this.GetAccountLogged();

        await _tenantService
            .UpdateOwnerAsync(
            accountLogged.Tenant.Id,
            request.NewOwnerId,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpPatch("tenants/name")]
    [SecureAuthorization]
    public async Task UpdateTenantNameAsync(UpdateTenantNameRequest request)
    {
        var accountLogged = this.GetAccountLogged();

        await _tenantService
            .UpdateNameByIdAndSaveAsync(
            accountLogged.Tenant.Id,
            request.NewName)
            .ConfigureAwait(false);
    }
}
