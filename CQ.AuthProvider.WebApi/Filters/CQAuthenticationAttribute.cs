using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.AuthProvider.DataAccess.Factory;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters;

internal class CQAuthenticationAttribute : SecureAuthenticationAttribute
{
    protected override async Task<bool> IsFormatOfHeaderValidAsync(string header, string headerValue, AuthorizationFilterContext context)
    {
        var authSection = base.GetService<AuthSection>(context);

        if (authSection.Logged!.IsActive)
        {
            return true;
        }

        var sessionService = base.GetService<ISessionService>(context);

        var isFormatValid = await sessionService.IsTokenValidAsync(headerValue).ConfigureAwait(false);

        return isFormatValid;
    }

    protected override async Task<object> GetRequestByHeaderAsync(string header, string headerValue, AuthorizationFilterContext context)
    {
        var authSection = base.GetService<AuthSection>(context);

        if (authSection.Logged!.IsActive)
        {
            return authSection.Logged.Account;
        }

        var accountService = base.GetService<IAccountService>(context);

        var account = await accountService.GetByTokenAsync(headerValue).ConfigureAwait(false);

        return account;
    }
}
