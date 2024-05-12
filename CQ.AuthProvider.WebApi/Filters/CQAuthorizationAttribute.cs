using CQ.ApiElements;
using CQ.ApiElements.Filters.Authorizations;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.Utility;
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
            var accountLogged = (Account)context.HttpContext.Items[ContextItems.AccountLogged];

            var permissionKey = new PermissionKey(permission);
            if (Guard.IsNotNull(accountLogged))
            {
                var hasPermissionAccount = accountLogged.HasPermission(permissionKey);

                return Task.FromResult(hasPermissionAccount);
            }

            var clientSystemLogged = (ClientSystem)context.HttpContext.Items[ContextItems.ClientSystemLogged];
            if (Guard.IsNull(clientSystemLogged))
            {
                return Task.FromResult(false);
            }

            var hasPermissionClient = clientSystemLogged.HasPermission(permissionKey);

            if(hasPermissionClient)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(clientSystemLogged.Name == headerValue);
        }
    }
}
