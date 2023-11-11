using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic;
using CQ.ApiElements.Filters.Extension;

namespace CQ.AuthProvider.WebApi.Controllers
{
    [ApiController]
    [Route("roles")]
    [CQAuthorization]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateRoleRequest request)
        {
            var role = request.Map();

            await this._roleService.CreateAsync(role).ConfigureAwait(false);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var roles = await this._roleService.GetAllAsync().ConfigureAwait(false);

            return Ok(roles.MapTo<MiniRoleResponse, MiniRole>());
        }
    }
}
