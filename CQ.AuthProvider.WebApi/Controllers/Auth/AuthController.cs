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
            var createAuth = request.Map();

            var auth = await this._authService.CreateAsync(createAuth);

            return Ok(auth);
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
