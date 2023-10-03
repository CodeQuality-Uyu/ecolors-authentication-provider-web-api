using Microsoft.AspNetCore.Mvc;
using PlayerFinder.Auth.Core;

namespace PlayerFinder.Auth.Api.Controllers
{
    [ApiController]
    [Route("token")]
    public class TokenController : ControllerBase
    {
        private readonly IAuthService _authService;

        public TokenController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]UserCredentialsRequest credentials)
        {
            var token = await this._authService.LoginAsync(credentials.Email, credentials.Password).ConfigureAwait(false);

            return Ok(new
            {
                token
            });
        }
    }

    public class UserCredentialsRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
