using CQ.ApiElements.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters
{
    public sealed class CQAuthExceptionFilter : ExceptionFilter
    {
        public CQAuthExceptionFilter(ExceptionStoreService exceptionStoreService, ExceptionRegistryService exceptionRegistryService) : base(exceptionStoreService, exceptionRegistryService)
        {
        }

        protected override ExceptionThrownContext BuildThrownContext(ExceptionContext context)
        {
            return new ExceptionThrownContext(context.Exception, context.RouteData.Values["controller"].ToString(), context.RouteData.Values["action"].ToString());
        }
    }
}
