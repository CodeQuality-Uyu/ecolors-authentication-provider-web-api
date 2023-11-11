using System.Net;

namespace CQ.AuthProvider.WebApi.Filters
{
    public record class ExceptionResponse
    {
        public readonly string Code;

        public readonly string Message;

        public readonly string LogMessage;

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

        public void SetContext(ExceptionThrownContext context)
        {
            this.Context = context;
        }
    }
}
