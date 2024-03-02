using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.ApiElements.Filters.Extension;
using CQ.AuthProvider.BusinessLogic.Authorizations;

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
        public async Task<List<PermissionResponse>> GetAllAsync()
        {
            var permissions = await this._permissionService.GetAllAsync().ConfigureAwait(false);

            return permissions.MapTo<PermissionResponse, Permission>();
        }
    }
}
