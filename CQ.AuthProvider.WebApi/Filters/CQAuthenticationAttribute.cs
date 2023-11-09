using CQ.ApiElements.Filters;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Principal;
using System.Text;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal class CQAuthenticationAttribute : AuthenticationAsyncAttributeFilter
    {
        protected IAuthService _authService { get; private set; } = null!;
        
        protected override async Task<bool> IsFormatOfTokenValidAsync(string token, AuthorizationFilterContext context)
        {
            var auth = await GetAuthOfTokenAsync(token, context).ConfigureAwait(false);

            context.HttpContext.Items[Items.Auth] = auth;

            return auth != null;
        }

        private async Task<Auth?> GetAuthOfTokenAsync(string token, AuthorizationFilterContext context)
        {
            this._authService = GetService<IAuthService>(context);

            try
            {
                var auth = await this._authService.GetMeAsync(token).ConfigureAwait(false);

                return auth;
            }
            catch (Exception) { return null; }
        }

        protected TService GetService<TService>(AuthorizationFilterContext context)
            where TService : class
        {
            return context.HttpContext.RequestServices.GetRequiredService<TService>();
        }
    }
}
