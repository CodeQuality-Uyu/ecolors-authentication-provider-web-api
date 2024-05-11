using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.ApiElements.Extensions;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.ApiElements;

namespace CQ.AuthProvider.WebApi.Controllers.Authorizations
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
        public async Task CreateAsync(CreatePermissionRequest request)
        {
            var permission = request.Map();

            await this._permissionService.CreateAsync(permission).ConfigureAwait(false);
        }

        [HttpPost("bulk")]
        public async Task CreateBulkAsync(CreatePermissionBulkRequest request)
        {
            var permissions = request.Map();

            await this._permissionService.CreateBulkAsync(permissions).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<List<PermissionResponse>> GetAllAsync(
            [FromQuery] bool @private,
            [FromQuery] string? roleId)
        {
            var accountLogged = base.HttpContext.GetItem<Account>(ContextItems.AccountLogged);

            var permissions = await this._permissionService.GetAllAsync(accountLogged, @private, roleId).ConfigureAwait(false);

            return permissions.MapTo<PermissionResponse, Permission>();
        }
    }
}
