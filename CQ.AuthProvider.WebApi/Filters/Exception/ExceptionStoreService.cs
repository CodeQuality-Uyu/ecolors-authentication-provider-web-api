
using CQ.AuthProvider.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal sealed class ExceptionStoreService
    {
        private readonly IDictionary<OriginError, ExceptionsOfOrigin> _specificExceptions = new Dictionary<OriginError, ExceptionsOfOrigin>();
        private readonly IDictionary<Type, ExceptionResponse> _genericExceptions = new Dictionary<Type, ExceptionResponse>();

        public void RegisterSolutionExceptions()
        {
            #region Specific exceptions
            this.AddOriginExceptions(new("Role","Post-roles"))
                .AddException<ResourceNotFoundException>(
                "ResourceNotFound",
                HttpStatusCode.Conflict,
                (ResourceNotFoundException exception, 
                ExceptionThrownContext context) => $"Invalid '{exception.Key}'. Resource '{exception.Resource}' was not found",
                (ResourceNotFoundException exception, 
                ExceptionThrownContext context) => $"The resource '{exception.Resource}' with '{exception.Key}': '{exception.Value}' not found"
                );
            #endregion

            #region Generic exceptions
            this.AddGenericException<ArgumentException>(
                    "InvalidArgument",
                    HttpStatusCode.InternalServerError,
                    (ArgumentException exception,
                    ExceptionThrownContext context) => $"Invalid argument '{exception.ParamName}'. {exception.Message}");

            this.AddGenericException<ArgumentNullException>(
                    "InvalidArgument",
                    HttpStatusCode.InternalServerError,
                    (ArgumentNullException exception,
                    ExceptionThrownContext context) => $"Invalid argument '{exception.ParamName}'. {exception.Message}");

            this.AddGenericException<InvalidRequestException>(
                    "InvalidRequest",
                    HttpStatusCode.BadRequest,
                    (InvalidRequestException exception,
                    ExceptionThrownContext context) => $"The prop '{exception.Prop}' has the following error '{exception.Error}'");

            this.AddGenericException<InvalidOperationException>(
                    "InterruptedOperationError",
                    HttpStatusCode.InternalServerError,
                    "The operation was interrupted due to an exception.");

            this.AddGenericException<ResourceNotFoundException>(
                    "ResourceNotFound",
                    HttpStatusCode.NotFound,
                    (ResourceNotFoundException exception,
                    ExceptionThrownContext context) => $"The resource with '{exception.Key}': '{exception.Value}' was not found",
                    (ResourceNotFoundException exception,
                    ExceptionThrownContext context) => $"The resource '{exception.Resource}' with '{exception.Key}': '{exception.Value}' not found");
            #endregion
        }

        private ExceptionsOfOrigin AddOriginExceptions(OriginError error)
        {
            var exceptionsOfOrigin = new ExceptionsOfOrigin();

            this._specificExceptions.Add(error, exceptionsOfOrigin);

            return exceptionsOfOrigin;
        }

        private void AddGenericException<TException>(
            string code,
            HttpStatusCode statusCode,
            Func<TException, ExceptionThrownContext, string> messageFunction,
            Func<TException, ExceptionThrownContext, string>? logMessageFunction = null)
            where TException : Exception
        {
            if (this._genericExceptions.ContainsKey(typeof(TException))) return;

            this._genericExceptions.Add(
                typeof(TException),
                new DinamicExceptionResponse<TException>(
                    code,
                    statusCode,
                    messageFunction,
                    logMessageFunction));
        }

        public void AddGenericException<TException>(
            string code,
            HttpStatusCode statusCode,
            string message,
            string? logMessage = null)
            where TException : Exception
        {
            if (this._genericExceptions.ContainsKey(typeof(TException))) return;

            this._genericExceptions.Add(
                typeof(TException),
                new ExceptionResponse(
                    code,
                    statusCode,
                    message,
                    logMessage));
        }

        public ExceptionResponse HandleException(ExceptionThrownContext context)
        {
            return this.HandleTypeException(context);
        }

        private ExceptionResponse HandleTypeException(ExceptionThrownContext context)
        {
            Exception exception = context.Exception;
            if (!this._genericExceptions.ContainsKey(exception.GetType()))
            {
                return new("ExceptionOccured", HttpStatusCode.InternalServerError, "An unpredicted exception ocurred");
            }

            var mapping = this._genericExceptions[exception.GetType()];

            mapping.SetContext(context);

            return mapping;
        }
    }
}
