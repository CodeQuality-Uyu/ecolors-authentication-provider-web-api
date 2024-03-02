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

        protected override async Task<bool> HasUserPermissionAsync(string token, string permission, AuthorizationFilterContext context)
        {
            var account = context.GetItem<AccountInfo>(ContextItems.AccountLogged);

            return account.Permissions.Contains(new PermissionKey(permission));
        }
    }
}
