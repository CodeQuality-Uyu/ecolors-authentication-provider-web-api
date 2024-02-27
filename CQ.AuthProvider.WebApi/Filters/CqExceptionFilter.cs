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
            return base.BuildThrownContext(context);
        }

        protected override void LogResponse(ExceptionResponse response)
        {
            Console.WriteLine(response.Context.Exception);
        }
    }
}
