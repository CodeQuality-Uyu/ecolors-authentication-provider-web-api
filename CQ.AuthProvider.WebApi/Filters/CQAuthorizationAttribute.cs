using CQ.ApiElements;
using CQ.ApiElements.Filters.Authorizations;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters;

internal class CQAuthorizationAttribute(string? permission = null)
    : SecureAuthorizationAttribute(
        new CQAuthenticationAttribute(),
        permission)
{
    protected override Task<bool> HasRequestPermissionAsync(string headerValue, string permission, AuthorizationFilterContext context)
    {
        var accountLogged = context.HttpContext.GetItem<AccountLogged>(ContextItems.AccountLogged);

        if (Guard.IsNull(accountLogged))
        {
            return Task.FromResult(false);
        }

        var permissionKey = new PermissionKey(permission);

        var hasPermissionAccount = accountLogged.HasPermission(permissionKey);

        return Task.FromResult(hasPermissionAccount);
    }
}
