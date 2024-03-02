using Microsoft.AspNetCore.Mvc;
using CQ.AuthProvider.WebApi.Filters;
using CQ.AuthProvider.WebApi.Extensions;

namespace CQ.AuthProvider.WebApi.Controllers.Accounts
{
    [ApiController]
    [Route("me")]
    [CQAuthentication]
    public class MeController : ControllerBase
    {
        [HttpGet]
        public AccountInfoResponse Get()
        {
            var accountLogged = this.GetAccountLogged();

            return new AccountInfoResponse(accountLogged);
        }

        [HttpPost("check-permission")]
        public object CheckPermissionAsync(CheckPermissionRequest request)
        {
            var mapped = request.Map();

            var account = this.GetAccountLogged();

            var hasPermission = true;
            try
            {
                account.AssertPermission(mapped);
            }
            catch (Exception)
            {
                hasPermission = false;
            }

            return new
            {
                hasPermission = hasPermission
            };
        }
    }
}
