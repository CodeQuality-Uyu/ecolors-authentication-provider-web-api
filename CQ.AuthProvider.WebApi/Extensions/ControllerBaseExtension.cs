using CQ.ApiElements;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Extensions
{
    public static class ControllerBaseExtension
    {
        public static Account GetAccountLogged(this ControllerBase controller)
        {
            return controller.GetItem<Account>(ContextItems.AccountLogged);
        }

        public static ClientSystem GetClientSystemLogged(this ControllerBase controller)
        {
            return controller.GetItem<ClientSystem>(ContextItems.ClientSystemLogged);
        }
    }
}
