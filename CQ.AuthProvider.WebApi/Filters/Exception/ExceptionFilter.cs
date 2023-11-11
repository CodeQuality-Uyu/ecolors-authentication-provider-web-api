using CQ.ApiElements.Filters;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal sealed class ExceptionFilter : IExceptionFilter
    {
        private readonly ExceptionStoreService _exceptionStoreService;

        public ExceptionFilter(ExceptionStoreService exceptionStoreService)
        {
            this._exceptionStoreService = exceptionStoreService;
            this._exceptionStoreService.RegisterSolutionExceptions();
        }

        public void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                return;
            }

            var response = HandleException(context);

            context.Result = new ObjectResult(new
            {
                Code = response.Code,
                Message = response.Message
            })
            {
                StatusCode = (int)response.StatusCode,
            };
        }

        private ExceptionResponse HandleException(ExceptionContext context)
        {
            var customcontext = new ExceptionThrownContext(
                context.Exception,
                context.RouteData.Values["controller"].ToString(),
                $"{context.RouteData.Values["action"]}-{context.HttpContext.Request.Path.ToString().Substring(1)}");

            return this._exceptionStoreService.HandleException(customcontext);
        }
    }
}
