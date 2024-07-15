using CQ.ApiElements;
using CQ.ApiElements.Filters.Authorizations;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
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

        var permissionKey = new PermissionKey(permission);

        var hasPermissionAccount = accountLogged.HasPermission(permissionKey);

        return Task.FromResult(hasPermissionAccount);
    }
}
