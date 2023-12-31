using CQ.ApiElements.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters
{
    public sealed class CqExceptionFilter : ExceptionFilter
    {
        public CqExceptionFilter(ExceptionStoreService exceptionStoreService, ExceptionRegistryService exceptionRegistryService) : base(exceptionStoreService, exceptionRegistryService)
        {
        }

        protected override ExceptionThrownContext BuildThrownContext(ExceptionContext context)
        {
            Console.WriteLine(context.Exception.GetType());

            return base.BuildThrownContext(context);
        }
    }
}
