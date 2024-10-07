using CQ.ApiElements.Filters.Authentications;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.BusinessLogic.Tokens;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters;

internal sealed class CQAuthenticationAttribute
    : SecureAuthenticationAttribute
{
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
        var sessionService = context.GetService<ISessionService>();

        var account = await sessionService
            .GetAccountByTokenAsync(headerValue)
            .ConfigureAwait(false);

        if (!account.HasTenant)
        {
            throw new InvalidOperationException("Account incomplete, missing tenant");
        }

        return account;
    }
}
