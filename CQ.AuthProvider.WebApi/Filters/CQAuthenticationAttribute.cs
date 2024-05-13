using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.AuthProvider.BusinessLogic.Sessions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal class CQAuthenticationAttribute : SecureAuthenticationAsyncAttributeFilter
    {
        protected override async Task<bool> IsFormatOfHeaderValidAsync(string header, string headerValue, AuthorizationFilterContext context)
        {
            var sessionService = base.GetService<ISessionService>(context);

            var isFormatValid = await sessionService.IsTokenValidAsync(headerValue).ConfigureAwait(false);

            return isFormatValid;
        }

        protected override async Task<object> GetRequestByHeaderAsync(string header, string headerValue, AuthorizationFilterContext context)
        {
            if (header == "Authorization")
            {
                var accountService = base.GetService<IAccountService>(context);

                var account = await accountService.GetByTokenAsync(headerValue).ConfigureAwait(false);

                return account;
            }

            var authOptions = base.GetService<AuthOptions>(context);

            if(authOptions.PrivateKey == headerValue)
            {
                return new ClientSystem()
                {
                    Name = headerValue
                };
            }

            var clientSystemService = base.GetService<IClientSystemService>(context);

            var clientSystem = await clientSystemService.GetByPrivateKeyAsync(headerValue).ConfigureAwait(false);

            return clientSystem;
        }
    }
}
