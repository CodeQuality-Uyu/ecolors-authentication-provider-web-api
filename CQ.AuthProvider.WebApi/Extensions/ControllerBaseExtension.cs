using CQ.AuthProvider.BusinessLogic.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Extensions
{
    public static class ControllerBaseExtension
    {
        public static Account? GetAuthLogged(this ControllerBase controller)
        {
            return controller.HttpContext.Items[Items.Auth] as Account;
        }
    }
}
