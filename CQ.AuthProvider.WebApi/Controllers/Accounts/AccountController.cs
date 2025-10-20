using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.ApiElements.Filters.Authentications;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.AuthProvider.WebApi.Controllers.Sessions;
using CQ.AuthProvider.WebApi.Controllers.Apps;
using CQ.AuthProvider.BusinessLogic.Apps;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts;

[ApiController]
[Route("accounts")]
public sealed class AccountController(
    IAccountService accountService,
    IAppService appService,
    [FromKeyedServices(MapperKeyedService.Presentation)] IMapper _mapper)
    : ControllerBase
{
    #region New accounts with tenant of account logged
    [HttpPost("credentials")]
    public async Task<SessionCreatedResponse> CreateCredentialsAsync(CreateAccountArgs request)
    {
        var account = await accountService
            .CreateAndSaveAsync(request)
            .ConfigureAwait(false);

        return _mapper.Map<SessionCreatedResponse>(account);
    }

    [HttpPost("credentials/for")]
    [BearerAuthentication]
    [SecureAuthorization]
    public async Task<CreateCredentialsForResponse> CreateCredentialsForAsync(CreateAccountForArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        var accountCreated = await accountService
            .CreateAndSaveAsync(request, accountLogged)
            .ConfigureAwait(false);

        return _mapper.Map<CreateCredentialsForResponse>(accountCreated);
    }
    #endregion New accounts with tenant of account logged

    #region New accounts with new tenant
    [HttpPost("credentials-with-tenant")]
    public async Task<SessionCreatedResponse> CreateCredentialsWithTenantAsync(CreateAccountWithTenantArgs request)
    {
        var account = await accountService
            .CreateAndSaveWithTenantAsync(request)
            .ConfigureAwait(false);

        return _mapper.Map<SessionCreatedResponse>(account);
    }
    #endregion New accounts with new tenant

    [HttpGet]
    [BearerAuthentication]
    [SecureAuthorization]
    public async Task<Pagination<AccountBasicInfoResponse>> GetAllAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var accountLogged = this.GetAccountLogged();

        var accounts = await accountService
            .GetAllAsync(page, pageSize, accountLogged)
            .ConfigureAwait(false);

        return _mapper.Map<Pagination<AccountBasicInfoResponse>>(accounts);
    }

    [HttpPatch("{id}/roles")]
    [BearerAuthentication]
    [SecureAuthorization]
    public async Task UpdateRolesAsync(
        Guid id,
        UpdateRolesArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        await accountService
            .UpdateRolesAsync(
            id,
            request,
            accountLogged)
            .ConfigureAwait(false);
    }

    [HttpGet("{email}/apps")]
    public async Task<List<AppDetailInfoResponse>> GetAppsWhereAccountBelongsAsync(string email)
    {
        var accounts = await appService
            .GetByEmailAccountAsync(email)
            .ConfigureAwait(false);

        return _mapper.Map<List<AppDetailInfoResponse>>(accounts);
    }
}
