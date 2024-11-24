using AutoMapper;
using CQ.ApiElements;
using CQ.ApiElements.Filters.Authentications;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Me;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;
using CQ.AuthProvider.WebApi.Controllers.Me.Models;
using CQ.AuthProvider.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Controllers.Me;

[ApiController]
[Route("me")]
public sealed class MeController(
    IMeService _meService,
    ITenantService _tenantService,
    IMapper _mapper)
    : ControllerBase
{
    [HttpGet]
    public AccountLoggedResponse GetMeAsync()
    {
        var accountLogged = this.GetAccountLogged();

        return _mapper.Map<AccountLoggedResponse>(accountLogged);
    }

    [HttpPatch("password")]
    [BearerAuthentication]
    public async Task UpdatePasswordAsync(UpdatePasswordLoggedRequest request)
    {
        var args = request.Map();

        var accountLogged = this.GetAccountLogged();
        
        await _meService
            .UpdatePasswordLoggedAsync(args, accountLogged)
            .ConfigureAwait(false);
    }


    [HttpPatch("tenants/owner")]
    [BearerAuthentication]
    [SecureAuthorization(ContextItem.AccountLogged)]
    public async Task TransferTenantAsync(TransferTenantRequest request)
    {
        var args = request.Map();

        var accountLogged = this.GetAccountLogged();

        await _tenantService
            .UpdateOwnerAsync(
            accountLogged.TenantValue.Id,
            args,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpPatch("tenants/name")]
    [BearerAuthentication]
    [SecureAuthorization(ContextItem.AccountLogged)]
    public async Task UpdateTenantNameAsync(UpdateTenantNameRequest request)
    {
        var args = request.Map();

        var accountLogged = this.GetAccountLogged();

        await _tenantService
            .UpdateNameByIdAndSaveAsync(
            accountLogged.TenantValue.Id,
            args)
            .ConfigureAwait(false);
    }
}
