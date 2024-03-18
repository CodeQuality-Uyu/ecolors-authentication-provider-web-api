using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CQ.AuthProvider.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ClientSystemAuthorization : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string? _permission;

        public ClientSystemAuthorization()
        {
        }

        public ClientSystemAuthorization(string permission)
        {
            this._permission = permission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var privateKey = context.HttpContext.Request.Headers["PrivateKey"];

            if (Guard.IsNullOrEmpty(privateKey))
            {
                context.Result = new ObjectResult(new
                {
                    Code = "Unauthenticated",
                    Message = "Missing private key header"
                })
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };

                return;
            }

            var systemService = context.HttpContext.RequestServices.GetRequiredService<IClientSystemService>();

            var system = await systemService.GetByPrivateKeyAsync(privateKey).ConfigureAwait(false);

            var permission = this._permission ?? $"{context.RouteData.Values["action"].ToString().ToLower()}-{context.RouteData.Values["controller"].ToString().ToLower()}";

            var missingPermission = !system.HasPermission(new (permission));
            if (missingPermission)
            {
                context.Result = new ObjectResult(new
                {
                    Code = "Unauthorized",
                    Message = $"Missing permission {permission}"
                })
                {
                    StatusCode = (int)HttpStatusCode.Forbidden
                };

                return;
            }
        }
    }
}
