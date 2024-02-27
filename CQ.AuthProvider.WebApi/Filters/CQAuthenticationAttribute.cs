using CQ.ApiElements.Filters;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.WebApi.Extensions;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Principal;
using System.Text;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal class CQAuthenticationAttribute : AuthenticationAsyncAttributeFilter
    {
        public CQAuthenticationAttribute() : base() { }

        public CQAuthenticationAttribute(string permission) : base(permission) { }

        protected override async Task<bool> IsFormatOfTokenValidAsync(string token, AuthorizationFilterContext context)
        {
            if (context.HttpContext.Items[Items.Auth] != null) return true;

            var auth = await GetAuthOfTokenAsync(token, context).ConfigureAwait(false);

            context.HttpContext.Items[Items.Auth] = auth;

            return true;
        }

        private async Task<Account> GetAuthOfTokenAsync(string token, AuthorizationFilterContext context)
        {
            var authService = base.GetService<IAccountService>(context);

            var auth = await authService.GetMeAsync(token).ConfigureAwait(false);

            return auth;
        }
    }
}
