using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;
using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts;

[ApiController]
[Route("accounts")]
public class AccountController(
    IMapper mapper,
    IAccountService accountService) : ControllerBase
{
    [HttpPost("credentials")]
    public async Task<AccountCreatedResponse> CreateCredentialsAsync(CreateAccountRequest request)
    {
        var createAccount = request.Map();

        var account = await accountService
            .CreateAsync(createAccount)
            .ConfigureAwait(false);

        return mapper.Map<AccountCreatedResponse>(account);
    }

    [HttpPost("credentials/for")]
    [CQAuthorization]
    public async Task CreateCredentialsForAsync(CreateAccountRequest request)
    {
        var createAccountFor = request.Map();

        await accountService
            .CreateAsync(createAccountFor)
            .ConfigureAwait(false);
    }

    [HttpGet("me")]
    [CQAuthentication]
    public AccountLoggedResponse GetMeAsync()
    {
        var accountLogged = this.GetAccountLogged();

        return mapper.Map<AccountLoggedResponse>(accountLogged);
    }
}
