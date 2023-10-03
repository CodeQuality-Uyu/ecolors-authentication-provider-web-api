using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic;
using CQ.ApiFilters.Core;
using System.ComponentModel.DataAnnotations;

namespace PlayerFinder.Auth.Api.Controllers
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

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateAuthRequest request)
        {
            var auth = await this._authService.CreateAsync(new AuthUser
            {
                Email = request.Email,
                Password = request.Password,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
            });

            return Ok(auth);
        }
        

        [HttpPut("password")]
        [AuthenticationHandlerFilter]
        public async Task<IActionResult> UpdatePasswordAsync([FromHeader] string authorization, [FromBody] UpdatePasswordRequest request)
        {
            var userLogged = await this._authService.DeserializeTokenAsync(authorization).ConfigureAwait(false);

            await this._authService.UpdatePasswordAsync(request.Password, userLogged).ConfigureAwait(false);

            return NoContent();
        }

        public class ResetPasswordRequestUnauthenticated
        {
            public string Email { get; set; }
        }

        public class UpdatePasswordRequest
        {
            public string Password { get; set; }
        }

        public class CreateAuthRequest
        {
            public string Email { get; set; }

            public string Password { get; set; }
            
            public string Firstname { get; set; }
            
            public string Lastname { get; set; }
        }
    }
}
