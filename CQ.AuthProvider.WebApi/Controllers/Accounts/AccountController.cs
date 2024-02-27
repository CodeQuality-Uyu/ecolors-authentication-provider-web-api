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
        public async Task<IActionResult> CreateCredentialsAsync(CreateAccountRequest request)
        {
            var createAuth = request.Map();

            var auth = await this._accountService.CreateAsync(createAuth);

            return Ok(auth);
        }
    }
}
