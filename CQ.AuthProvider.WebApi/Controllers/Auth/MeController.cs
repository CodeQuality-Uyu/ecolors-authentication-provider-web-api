using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic;

namespace CQ.AuthProvider.WebApi.Controllers
{
    [ApiController]
    [Route("me")]
    [CQAuthentication]
    public class MeController : ControllerBase
    {
        private readonly IAuthService _authService;

        public MeController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.GetAuthLogged());
        }

        [HttpPost("check-permission")]
        public async Task<IActionResult> CreateAsync(CheckPermissionRequest request)
        {
            var hasPermission = await this._authService.HasPermissionAsync(request.Permission, this.GetAuthLogged());

            return Ok(new
            {
                hasPermission,
            });
        }
    }
}
