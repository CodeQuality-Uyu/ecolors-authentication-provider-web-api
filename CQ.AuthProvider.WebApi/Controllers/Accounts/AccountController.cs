using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;
using AutoMapper;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.WebApi.Controllers.Me.Models;
using CQ.Utility;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts;

[ApiController]
[Route("accounts")]
public class AccountController(
    IAccountService _accountService,
    IMapper mapper)
    : ControllerBase
{
    [HttpPost("credentials")]
    public async Task<AccountCreatedResponse> CreateCredentialsAsync(CreateAccountRequest request)
    {
        var createAccount = request.Map();

        var account = await _accountService
            .CreateAndSaveAsync(createAccount)
            .ConfigureAwait(false);

        return mapper.Map<AccountCreatedResponse>(account);
    }

    [HttpPatch("password")]
    public async Task UpdatePasswordAsync(UpdatePasswordRequest? request)
    {
        Guard.ThrowIsNull(request, nameof(request));

        var args = request.Map();

        await _accountService
            .UpdatePasswordByCredentialsAsync(args)
            .ConfigureAwait(false);
    }

    [HttpPost("credentials/for")]
    [SecureAuthorization]
    public async Task CreateCredentialsForAsync(CreateAccountRequest request)
    {
        var createAccountFor = request.Map();

        await _accountService
            .CreateAndSaveAsync(createAccountFor)
            .ConfigureAwait(false);
    }
}
