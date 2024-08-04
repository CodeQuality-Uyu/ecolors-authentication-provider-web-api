using CQ.ApiElements;
using CQ.ApiElements.Filters.Authorizations;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters;

internal sealed class CQAuthorizationAttribute(string? permission = null)
    : SecureAuthorizationAttribute(permission)
{
    protected override Task<bool> HasRequestPermissionAsync(
        string headerValue,
        string permission,
        AuthorizationFilterContext context)
    {
        var accountLogged = context.GetItem<AccountLogged>(ContextItems.AccountLogged);

        var hasPermissionAccount = accountLogged.HasPermission(permission);

        return Task.FromResult(hasPermissionAccount);
    }
}
