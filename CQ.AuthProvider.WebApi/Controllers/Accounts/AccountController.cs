using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Models;
using AutoMapper;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts;

[ApiController]
[Route("accounts")]
public class AccountController(
    IAccountService accountService,
    IMapper mapper)
    : ControllerBase
{
    [HttpPost("credentials")]
    public async Task<AccountCreatedResponse> CreateCredentialsAsync(CreateAccountRequest request)
    {
        var createAccount = request.Map();

        var account = await accountService
            .CreateAndSaveAsync(createAccount)
            .ConfigureAwait(false);

        return mapper.Map<AccountCreatedResponse>(account);
    }

    [HttpPost("credentials/for")]
    [SecureAuthorization]
    public async Task CreateCredentialsForAsync(CreateAccountRequest request)
    {
        var createAccountFor = request.Map();

        await accountService
            .CreateAndSaveAsync(createAccountFor)
            .ConfigureAwait(false);
    }
}
