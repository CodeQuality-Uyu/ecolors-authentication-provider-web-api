using AutoMapper;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Me;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;
using CQ.AuthProvider.WebApi.Controllers.Me.Models;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.AuthProvider.WebApi.Filters;
using CQ.Utility;
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
    public async Task UpdatePasswordAsync(UpdatePasswordRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var args = request.Map();

        await _meService
            .UpdatePasswordAsync(args)
            .ConfigureAwait(false);
    }


    [HttpPatch("tenants/owner")]
    [CQAuthentication]
    [SecureAuthorization]
    public async Task TransferTenantAsync(TransferTenantRequest request)
    {
        var args = request.Map();

        var accountLogged = this.GetAccountLogged();

        await _meService
            .TransferTenantAsync(args, accountLogged)
            .ConfigureAwait(false);
    }

    [HttpPatch("tenants/name")]
    [CQAuthentication]
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
