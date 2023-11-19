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
        public async Task CreateAsync(CreatePermissionRequest request)
        {
            var role = request.Map();

            await this._permissionService.CreateAsync(role).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<IList<PermissionResponse>> GetAsync()
        {
            var permissions = await this._permissionService.GetAllAsync().ConfigureAwait(false);

            return permissions.MapTo<PermissionResponse, Permission>();
        }

        [HttpGet("public")]
        public async Task<IList<MiniPermissionResponse>> GetAllPublicAsync()
        {
            var permissions = await this._permissionService.GetAllPublicAsync().ConfigureAwait(false);

            return permissions.MapTo<MiniPermissionResponse, MiniPublicPermission>();
        }
    }
}
