using CQ.ApiElements.Filters.Authentications;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.AuthProvider.DataAccess.Factory;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters;

internal sealed class CQAuthenticationAttribute : SecureAuthenticationAttribute
{
    protected override async Task<object?> GetFakeAuthAsync(AuthorizationFilterContext context)
    {
        var authSection = context.GetService<AuthSection>();

        if (!authSection.Fake!.IsActive)
        {
            return null;
        }

        dynamic fakeAccount = authSection.Fake.Account;

        if (Guard.IsNullOrEmpty(fakeAccount.Id) &&
            Guard.IsNullOrEmpty(fakeAccount.Token))
        {
            return fakeAccount;
        }

        var accountService = context.GetService<IAccountService>();

        try
        {
            if (Guard.IsNotNullOrEmpty(fakeAccount.Token))
            {
                fakeAccount = await accountService
                    .GetByTokenAsync(fakeAccount.Token)
                    .ConfigureAwait(false);
            }

            fakeAccount = await accountService
                .GetByIdAsync(fakeAccount.Id)
                .ConfigureAwait(false);
        }
        catch (System.Exception)
        {
        }

        return fakeAccount;
    }

    protected override async Task<bool> IsFormatOfHeaderValidAsync(
        string header,
        string headerValue,
        AuthorizationFilterContext context)
    {
        var sessionService = context.GetService<ISessionService>();

        var isFormatValid = await sessionService
            .IsTokenValidAsync(headerValue)
            .ConfigureAwait(false);

        return isFormatValid;
    }

    protected override async Task<object> GetItemByHeaderAsync(
        string header,
        string headerValue,
        AuthorizationFilterContext context)
    {
        var accountService = context.GetService<IAccountService>();

        var account = await accountService
            .GetByTokenAsync(headerValue)
            .ConfigureAwait(false);

        return account;
    }
}
