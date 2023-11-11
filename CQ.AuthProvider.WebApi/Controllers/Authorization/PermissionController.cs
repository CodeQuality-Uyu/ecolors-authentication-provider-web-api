using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic;
using CQ.ApiElements.Filters.Extension;

namespace CQ.AuthProvider.WebApi.Controllers
{
    [ApiController]
    [Route("permissions")]
    [CQAuthorization]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            this._permissionService = permissionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePermissionRequest request)
        {
            var role = request.Map();

            await this._permissionService.CreateAsync(role).ConfigureAwait(false);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var permissions = await this._permissionService.GetAllAsync().ConfigureAwait(false);
            
            return Ok(permissions.MapTo<MiniPermissionResponse, MiniPermission>());
        }
    }
}
