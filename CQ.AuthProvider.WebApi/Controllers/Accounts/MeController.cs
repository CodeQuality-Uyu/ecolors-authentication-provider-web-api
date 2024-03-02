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
        [HttpGet]
        public AccountInfo Get()
        {
            return this.GetAccountLogged();
        }

        [HttpPost("check-permission")]
        public object CreateAsync(CheckPermissionRequest request)
        {
            var mapped = request.Map();

            var account = this.GetAccountLogged();

            return new
            {
                hasPermission = account.Permissions.Contains(mapped)
            };
        }
    }
}
