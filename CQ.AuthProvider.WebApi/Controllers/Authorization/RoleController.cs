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
        public async Task CreateAsync(CreateRoleRequest request)
        {
            var role = request.Map();

            await this._roleService.CreateAsync(role).ConfigureAwait(false);
        }

        [HttpPost("{id}/permissions")]
        public async Task AddPermissionAsync(string id, AddPermissionRequest request)
        {
            var requestMapped = request.Map();

            await this._roleService.AddPermissionByIdAsync(id, requestMapped).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<IList<RoleResponse>> GetAsync()
        {
            var roles = await this._roleService.GetAllAsync().ConfigureAwait(false);

            return roles.MapTo<RoleResponse, Role>();
        }

        [HttpGet("public")]
        public async Task<IList<MiniPublicRoleResponse>> GetAllPublicAsync()
        {
            var roles = await this._roleService.GetAllPublicAsync().ConfigureAwait(false);

            return roles.MapTo<MiniPublicRoleResponse, MiniRole>();
        }
    }
}
