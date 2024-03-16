using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.BusinessLogic.Accounts;

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

            var account = await this._accountService.CreateAsync(createAccount);

            return new CreateAccountResponse(account);
        }
    }
}
