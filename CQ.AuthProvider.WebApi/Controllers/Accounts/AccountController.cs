using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;
using AutoMapper;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Utils;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts;

[ApiController]
[Route("accounts")]
public class AccountController(
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
    [SecureAuthorization]
    public async Task CreateCredentialsForAsync(CreateAccountArgs request)
    {
        await _accountService
            .CreateAndSaveAsync(request)
            .ConfigureAwait(false);
    }
}
