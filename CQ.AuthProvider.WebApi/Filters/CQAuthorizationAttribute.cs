using CQ.ApiElements;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal class CQAuthorizationAttribute : SecureAuthorizationAsyncAttributeFilter
    {
        public CQAuthorizationAttribute(string? permission = null)
            : base(
                  new CQAuthenticationAttribute(),
                  permission)
        {
        }

        protected override Task<bool> HasRequestPermissionAsync(string headerValue, string permission, AuthorizationFilterContext context)
        {
            var accountLogged = context.HttpContext.Items[ContextItems.AccountLogged];
            var clientSystemLogged = context.HttpContext.Items[ContextItems.ClientSystemLogged];

            var permissionKey = new PermissionKey(permission);
            if (accountLogged != null)
            {
                var hasPermissionAccount = ((Account)accountLogged).HasPermission(permissionKey);

                return Task.FromResult(hasPermissionAccount);
            }

            var hasPermissionClient = ((ClientSystem)clientSystemLogged).HasPermission(permissionKey);

            return Task.FromResult(hasPermissionClient);
        }
    }
}
