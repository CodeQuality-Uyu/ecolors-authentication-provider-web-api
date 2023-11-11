using System.Net;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal sealed record class DinamicExceptionResponse<TException> : ExceptionResponse
        where TException : Exception
    {
        private readonly Func<TException, ExceptionThrownContext, string> _messageFunction;

        private readonly Func<TException, ExceptionThrownContext, string> _logMessageFunction;

        public DinamicExceptionResponse(
            string code,
            HttpStatusCode statusCode,
            Func<TException, ExceptionThrownContext, string> messageFunction,
            Func<TException, ExceptionThrownContext, string>? logMessageFunction = null) : base(code, statusCode, string.Empty, string.Empty)
        {
            this._messageFunction = messageFunction;
            this._logMessageFunction = logMessageFunction ?? messageFunction;
        }

        public override void SetContext(ExceptionThrownContext context)
        {
            base.SetContext(context);

            this.Message = this.BuildMessage(this._messageFunction);
            this.LogMessage = this.BuildMessage(this._logMessageFunction);
        }

        private string BuildMessage(Func<TException, ExceptionThrownContext, string> messageFunction)
        {
            if (base.Context == null) return "Context of exception unknown";

            var exception = (TException)base.Context.Exception;

            if (exception == null) return $"Unknown exception: {base.Context?.Exception}";

            return messageFunction(exception, base.Context);
        }
    }
}
