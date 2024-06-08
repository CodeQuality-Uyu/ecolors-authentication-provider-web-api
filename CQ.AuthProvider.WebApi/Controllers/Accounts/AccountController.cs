using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.ApiElements.Filters.Authentications;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpPost("credentials")]
        public async Task<CreateAccountResponse> CreateCredentialsAsync(CreateAccountRequest request)
        {
            var createAccount = request.Map();

            var account = await _accountService.CreateAsync(createAccount);

            return new CreateAccountResponse(account);
        }

        [HttpPost("credentials/for")]
        [CQAuthorization]
        [ValidateAccount]
        public async Task CreateCredentialsForAsync(CreateAccountRequest request)
        {
            var createAccountFor = request.Map();

            await _accountService.CreateAsync(createAccountFor).ConfigureAwait(false);
        }

        [HttpGet("me")]
        [CQAuthentication]
        [ValidateAccount]
        public AccountResponse GetMeAsync()
        {
            var accountLogged = this.GetAccountLogged();

            return new AccountResponse(accountLogged);
        }
    }
}
