using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.WebApi.Filters;
using System.ComponentModel.DataAnnotations;

namespace CQ.AuthProvider.WebApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("credentials")]
        public async Task<IActionResult> CreateAsync(CreateAuthRequest request)
        {
            var createAuth = new CreateAuth(
               request.Email,
               request.Password,
               request.FirstName,
                request.LastName);

            var auth = await this._authService.CreateAsync(createAuth);

            return Ok(auth);
        }
        

        [HttpPut("password")]
        // [AuthenticationHandlerFilter]
        public async Task<IActionResult> UpdatePasswordAsync([FromHeader] string authorization, UpdatePasswordRequest request)
        {
            var userLogged = await this._authService.DeserializeTokenAsync(authorization).ConfigureAwait(false);

            await this._authService.UpdatePasswordAsync(request.Password, userLogged).ConfigureAwait(false);

            return NoContent();
        }
    }

    public class ResetPasswordRequestUnauthenticated
    {
        public string Email { get; set; }
    }

    public class UpdatePasswordRequest
    {
        public string Password { get; set; }
    }
}
