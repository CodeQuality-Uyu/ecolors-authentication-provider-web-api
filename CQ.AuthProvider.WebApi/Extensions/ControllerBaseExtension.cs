using CQ.ApiElements;
using CQ.ApiElements.Filters.Extensions;
using CQ.AuthProvider.BusinessLogic.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.WebApi.Extensions;

public static class ControllerBaseExtension
{
    public static AccountLogged GetAccountLogged(this ControllerBase controller)
    {
        return controller.GetItem<AccountLogged>(ContextItems.AccountLogged);
    }
}
