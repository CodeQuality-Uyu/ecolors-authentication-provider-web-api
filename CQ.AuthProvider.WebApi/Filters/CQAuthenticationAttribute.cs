using CQ.ApiElements.Filters;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal class CQAuthenticationAttribute : AuthenticationAsyncAttributeFilter
    {
        public CQAuthenticationAttribute() : base()
        {
        }

        public CQAuthenticationAttribute(string permission) : base(permission) { }

        protected override async Task<bool> IsFormatOfTokenValidAsync(string token, AuthorizationFilterContext context)
        {
            if (context.HttpContext.Items[ContextItems.AccountLogged] != null)
                return true;

            var account = await GetAuthOfTokenAsync(token, context).ConfigureAwait(false);

            return true;
        }

        protected override async Task<object> GetAccountByTokenAsync(string token, AuthorizationFilterContext context)
        {
            var authService = base.GetService<IAccountService>(context);

            var account = await authService.GetMeAsync(token).ConfigureAwait(false);

            return account;
        }
    }
}
