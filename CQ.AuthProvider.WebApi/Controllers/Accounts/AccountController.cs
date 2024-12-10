using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;
using AutoMapper;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.ApiElements.Filters.Authentications;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.AuthProvider.WebApi.Extensions;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts;

[ApiController]
[Route("accounts")]
public sealed class AccountController(
    IAccountService _accountService,
    [FromKeyedServices(MapperKeyedService.Presentation)] IMapper _mapper)
    : ControllerBase
{
    [HttpPost("credentials")]
    public async Task<AccountCreatedResponse> CreateCredentialsAsync(CreateAccountArgs request)
    {
        var account = await _accountService
            .CreateAndSaveAsync(request)
            .ConfigureAwait(false);

        return _mapper.Map<AccountCreatedResponse>(account);
    }

    [HttpPatch("me/password")]
    public async Task UpdatePasswordAsync(UpdatePasswordArgs request)
    {
        await _accountService
            .UpdatePasswordByCredentialsAsync(request)
            .ConfigureAwait(false);
    }

    [HttpPost("credentials/for")]
    [BearerAuthentication]
    [SecureAuthorization]
    public async Task CreateCredentialsForAsync(CreateAccountForArgs request)
    {
        var accountLogged = this.GetAccountLogged();

        await _accountService
            .CreateAndSaveAsync(request, accountLogged)
            .ConfigureAwait(false);
    }

    [HttpGet]
    [BearerAuthentication]
    [SecureAuthorization]
    public async Task<Pagination<AccountBasicInfoResponse>> GetAllAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var accountLogged = this.GetAccountLogged();

        var accounts = await _accountService
            .GetAllAsync(page, pageSize, accountLogged)
            .ConfigureAwait(false);

        return _mapper.Map<Pagination<AccountBasicInfoResponse>>(accounts);
    }
}
