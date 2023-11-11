using System.Net;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal record class ExceptionResponse
    {
        public readonly string Code;

        public string Message { get; protected set; }

        public string LogMessage { get; protected set; }

        public readonly HttpStatusCode StatusCode;

        public ExceptionThrownContext? Context;

        public ExceptionResponse(
            string code,
            HttpStatusCode statusCode,
            string message,
            string? logMessage = null)
        {
            this.Code = code;
            this.StatusCode = statusCode;
            this.Message = message;
            this.LogMessage = logMessage ?? message;
        }

        public virtual void SetContext(ExceptionThrownContext context)
        {
            this.Context = context;
        }
    }
}
