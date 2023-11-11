using CQ.AuthProvider.BusinessLogic;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.CompilerServices;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal sealed class CQAuthorizationAttribute : CQAuthenticationAttribute
    {
        private readonly string? _permission;

        public CQAuthorizationAttribute() { }

        public CQAuthorizationAttribute(string permission)
        {
            _permission = permission;
        }

        protected override async Task<(bool isAuthorized, string permission)> IsUserAuthorizedAsync(string token, AuthorizationFilterContext context)
        {
            var permission = this._permission ?? $"{context.HttpContext.Request.Method.ToLower()}-{context.HttpContext.Request.Path.Value.Substring(1)}";

            var auth = (Auth)context.HttpContext.Items[Items.Auth];

            try
            {
                var isAuthorized = await base._authService.HasPermissionAsync(permission, auth).ConfigureAwait(false);

                return (isAuthorized, permission);
            }
            catch (Exception ex) 
            { 
                return (false, permission);
            }
        }
    }
}
