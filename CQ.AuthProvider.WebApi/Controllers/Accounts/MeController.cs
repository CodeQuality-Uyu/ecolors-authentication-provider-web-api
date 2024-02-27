using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.WebApi.Extensions;

namespace CQ.AuthProvider.WebApi.Controllers
{
    [ApiController]
    [Route("me")]
    [CQAuthentication]
    public class MeController : ControllerBase
    {
        private readonly IAccountService _authService;

        public MeController(IAccountService authService)
        {
            this._authService = authService;
        }

        [HttpGet]
        public Account Get()
        {
            return this.GetAuthLogged();
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
