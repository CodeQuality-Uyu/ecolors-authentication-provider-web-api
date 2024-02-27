using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal class CQAuthorizationAttribute : CQAuthenticationAttribute
    {

        public CQAuthorizationAttribute() : base() { }

        public CQAuthorizationAttribute(string permission) : base(permission) { }

        protected override async Task<bool> HasUserPermissionAsync(string token, string permission, AuthorizationFilterContext context)
        {
            var authService = GetService<IAccountService>(context);

            var account = (Account)context.HttpContext.Items[Items.Auth];

            var isAuthorized = await authService.HasPermissionAsync(permission, account).ConfigureAwait(false);

            return isAuthorized;
        }
    }
}
