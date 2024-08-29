using CQ.ApiElements.Filters.Authentications;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tokens;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters;

internal sealed class CQAuthenticationAttribute
    : SecureAuthenticationAttribute
{
    protected override Task<object?> GetFakeAuthAsync(AuthorizationFilterContext context)
    {
        object? fakeAccount;
        try
        {
            fakeAccount = context.GetService<AccountLogged>();
        }
        catch (System.Exception)
        {
            fakeAccount = null;
        }

        return Task.FromResult(fakeAccount);
    }

    protected override async Task<bool> IsFormatOfHeaderValidAsync(
        string header,
        string headerValue,
        AuthorizationFilterContext context)
    {
        var sessionService = context.GetService<ITokenService>();

        var isFormatValid = await sessionService
            .IsValidAsync(headerValue)
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
