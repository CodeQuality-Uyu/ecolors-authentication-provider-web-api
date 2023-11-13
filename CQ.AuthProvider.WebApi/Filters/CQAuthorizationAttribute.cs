using CQ.AuthProvider.BusinessLogic;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal class CQAuthorizationAttribute : CQAuthenticationAttribute
    {

        public CQAuthorizationAttribute() : base() { }

        public CQAuthorizationAttribute(string permission) : base(permission) { }


        protected override async Task<bool> HasUserPermissionAsync(string token, string permission, AuthorizationFilterContext context)
        {
            var authService = GetService<IAuthService>(context);

            var auth = (Auth)context.HttpContext.Items[Items.Auth];

            var isAuthorized = await authService.HasPermissionAsync(permission, auth).ConfigureAwait(false);

            return isAuthorized;
        }
    }
}
