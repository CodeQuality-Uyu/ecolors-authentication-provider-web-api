using CQ.ApiElements;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal class CQAuthorizationAttribute : CQAuthenticationAttribute
    {
        public CQAuthorizationAttribute() : base()
        {
        }

        public CQAuthorizationAttribute(string permission) : base(permission)
        {
        }

        protected override Task<bool> HasUserPermissionAsync(string token, string permission, AuthorizationFilterContext context)
        {
            var account = context.HttpContext.GetItem<AccountInfo>(ContextItems.AccountLogged);

            var hasPermission = account.Permissions.Contains(new PermissionKey(permission));
            
            return Task.FromResult(hasPermission);
        }
    }
}
