using AutoMapper;
using CQ.ApiElements.Filters.Authentications;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;
using CQ.AuthProvider.WebApi.Controllers.Me.Models;
using CQ.AuthProvider.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CQ.AuthProvider.WebApi.Controllers.Me;

[ApiController]
[BearerAuthentication]
[Route("me")]
public sealed class MeController(
    IAccountService _accountService,
    ITenantService _tenantService,
    [FromKeyedServices(MapperKeyedService.Presentation)] IMapper _mapper)
    : ControllerBase
{
    [HttpGet]
    public AccountLoggedResponse GetMeAsync()
    {
        var accountLogged = this.GetAccountLogged();

        return _mapper.Map<AccountLoggedResponse>(accountLogged);
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
        var args = request.Map();

        var accountLogged = this.GetAccountLogged();

        await _tenantService
            .UpdateNameByIdAndSaveAsync(
            accountLogged.Tenant.Id,
            args)
            .ConfigureAwait(false);
    }
}
