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
            var role = request.Map();

            await this._permissionService.CreateAsync(role).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<List<PermissionResponse>> GetAllAsync(
            [FromQuery] bool @private,
            [FromQuery] string? roleId)
        {
            var accountLogged = base.HttpContext.GetItem<AccountInfo>(ContextItems.AccountLogged);

            var permissions = await this._permissionService.GetAllAsync(@private, roleId, accountLogged).ConfigureAwait(false);

            return permissions.MapTo<PermissionResponse, Permission>();
        }
    }
}
