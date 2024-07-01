using CQ.ApiElements.Filters.Authentications;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.AuthProvider.DataAccess.Factory;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters;

internal sealed class CQAuthenticationAttribute : SecureAuthenticationAttribute
{
    public CQAuthenticationAttribute()
    {
        var fakeSection = "Logged:IsActive";
    }

    protected override async Task<bool> IsFormatOfHeaderValidAsync(
        string header,
        string headerValue,
        AuthorizationFilterContext context)
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

        var accountService = base.GetService<IAccountService>(context);

        if (authSection.Logged!.IsActive)
        {
            dynamic fakeAccount = authSection.Logged.Account;

            try
            {
                if (Guard.IsNotNullOrEmpty(fakeAccount.Token))
                {
                    fakeAccount = await accountService
                        .GetByTokenAsync(fakeAccount.Token)
                        .ConfigureAwait(false);
                }
                else if (Guard.IsNotNullOrEmpty(fakeAccount.Id))
                {
                    fakeAccount = await accountService
                        .GetByIdAsync(fakeAccount.Id)
                        .ConfigureAwait(false);
                }
            }
            catch (System.Exception)
            {
            }

            return fakeAccount;
        }

        var account = await accountService.GetByTokenAsync(headerValue).ConfigureAwait(false);

        return account;
    }
}
