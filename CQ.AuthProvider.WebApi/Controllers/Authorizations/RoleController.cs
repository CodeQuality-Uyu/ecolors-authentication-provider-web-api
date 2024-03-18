using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.WebApi.Extensions;
using CQ.ApiElements.Extensions;
using System.Data;

namespace CQ.AuthProvider.WebApi.Controllers.Authorizations
{
    [ApiController]
    [Route("roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        [HttpPost]
        [CQAuthorization]
        public async Task CreateAsync(CreateRoleRequest request)
        {
            var role = request.Map();

            await this._roleService.CreateAsync(role).ConfigureAwait(false);
        }

        [HttpPost("bulk")]
        [ClientSystemAuthorization]
        public async Task CreateBulkAsync(CreateRoleBulkRequest request)
        {
            var roles = request.Map();

            await this._roleService.CreateBulkAsync(roles).ConfigureAwait(false);
        }

        [HttpPost("{id}/permissions")]
        [CQAuthorization]
        public async Task AddPermissionAsync(string id, AddPermissionRequest request)
        {
            var requestMapped = request.Map();

            await this._roleService.AddPermissionByIdAsync(id, requestMapped).ConfigureAwait(false);
        }

        [HttpGet]
        [CQAuthorization]
        public async Task<List<RoleResponse>> GetAllAsync([FromQuery]bool @private)
        {
            var accountLogged = this.GetAccountLogged()!;

            var roles = await this._roleService.GetAllAsync(accountLogged, @private).ConfigureAwait(false);

            return roles.MapTo<RoleResponse, RoleInfo>();
        }
    }
}
