using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic;

namespace CQ.AuthProvider.WebApi.Controllers
{
    [ApiController]
    [Route("auths")]
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

        [HttpPost("check-permission")]
        [CQAuthentication]
        public async Task<IActionResult> CreateAsync(CheckPermissionRequest request)
        {
            var hastPermission = await this._authService.HasPermissionAsync(request.Permission, this.GetAuthLogged());

            return Ok(new
            {
                hastPermission,
            });
        }

        [HttpGet("me")]
        [CQAuthentication]
        public IActionResult Get()
        {
            return Ok(this.GetAuthLogged());
        }
    }

    internal static class ControllerBaseExtensions
    {
        public static Auth? GetAuthLogged(this ControllerBase controller)
        {
            return controller.HttpContext.Items[Items.Auth] as Auth;
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
