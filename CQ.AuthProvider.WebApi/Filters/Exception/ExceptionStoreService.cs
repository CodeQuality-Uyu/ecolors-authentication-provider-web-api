
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Authorization.Exceptions;
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

        public ExceptionStoreService()
        {
            this.RegisterSolutionExceptions();
        }

        public void RegisterSolutionExceptions()
        {
            #region Specific exceptions
            #region Role controller
            this.AddOriginExceptions(new("Role", "POST-roles"))
                .AddException<PermissionNotFoundException>(
                "ResourceNotFound",
                HttpStatusCode.Conflict,
                (PermissionNotFoundException exception,
                ExceptionThrownContext context) => $"The following permissions are incorrect '{string.Join(',', exception.PermissionKeys)}'"
                );
            #endregion

            #region Auth controller
            this.AddOriginExceptions(
                new("Auth", "POST-auths/credentials"))
                .AddException<InvalidCredentialsException>(
                "InvalidOperation",
                HttpStatusCode.Conflict,
                (InvalidCredentialsException exception,
                ExceptionThrownContext context) => $"The creation of the account was interrupted",
                (InvalidCredentialsException exception,
                ExceptionThrownContext context) => $"The account with '{exception.Email}' was not found"
                )
                .AddException<AuthDisabledException>(
                "InvalidOperation",
                HttpStatusCode.Conflict,
                (AuthDisabledException exception,
                ExceptionThrownContext context) => $"The creation of the account was interrupted",
                (AuthDisabledException exception,
                ExceptionThrownContext context) => $"The account with '{exception.Email}' is disabled"
                )
                .AddException<SpecificResourceNotFoundException<Role>>(
                "InvalidOperation",
                HttpStatusCode.Conflict,
                (SpecificResourceNotFoundException<Role> exception,
                ExceptionThrownContext context) => "The role specified does not exist");
            #endregion
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
                    "InterruptedOperation",
                    HttpStatusCode.InternalServerError,
                    "The operation was interrupted due to an exception.");

            this.AddGenericException<ResourceNotFoundException>(
                    "ResourceNotFound",
                    HttpStatusCode.NotFound,
                    (ResourceNotFoundException exception,
                    ExceptionThrownContext context) => $"The resource with '{exception.Key}': '{exception.Value}' was not found",
                    (ResourceNotFoundException exception,
                    ExceptionThrownContext context) => $"The resource '{exception.Resource}' with '{exception.Key}': '{exception.Value}' not found");

            this.AddGenericException<ResourceDuplicatedException>(
                    "ResourceDuplicated",
                    HttpStatusCode.Conflict,
                    (ResourceDuplicatedException exception,
                    ExceptionThrownContext context) => $"Exist a resource with '{exception.Key}': '{exception.Value}'",
                    (ResourceDuplicatedException exception,
                    ExceptionThrownContext context) => $"Exist a resource '{exception.Resource}' with '{exception.Key}': '{exception.Value}'");

            this.AddGenericException<InvalidCredentialsException>(
                "InvalidCredentials",
                HttpStatusCode.BadRequest,
                (InvalidCredentialsException exception,
                ExceptionThrownContext context) => $"The credentials provided are incorrect"
                );

            this.AddGenericException<AuthDisabledException>(
                "AccountDisabled",
                HttpStatusCode.BadRequest,
                (AuthDisabledException exception,
                ExceptionThrownContext context) => $"The account is disabled",
                (AuthDisabledException exception,
                ExceptionThrownContext context) => $"The account with '{exception.Email}' is disabled"
                );
            #endregion
        }

        private ExceptionsOfOrigin AddOriginExceptions(OriginError error)
        {
            if (this._specificExceptions.ContainsKey(error)) return this._specificExceptions[error];

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
            var exception = this.HandleSpecificException(context);

            exception ??= this.HandleTypeException(context);

            exception ??= new("ExceptionOccured", HttpStatusCode.InternalServerError, "An unpredicted exception ocurred");

            return exception;
        }

        private ExceptionResponse? HandleSpecificException(ExceptionThrownContext context)
        {
            var exception = context.Exception;
            var originError = new OriginError(context.ControllerName, context.Action);
            if (!this._specificExceptions.ContainsKey(originError))
            {
                return null;
            }

            var originErrorFound = this._specificExceptions[originError];

            if (!originErrorFound.Exceptions.ContainsKey(exception.GetType()))
            {
                return null;
            }

            var mapping = originErrorFound.Exceptions[exception.GetType()];

            mapping.SetContext(context);

            return mapping;
        }

        private ExceptionResponse? HandleTypeException(ExceptionThrownContext context)
        {
            var exception = context.Exception;
            var registeredType = this.GetRegisteredType(exception.GetType());

            if (registeredType == null) return null;

            var mapping = this._genericExceptions[registeredType];

            mapping.SetContext(context);

            return mapping;
        }

        private Type? GetRegisteredType(Type initialType)
        {
            if (initialType == typeof(Exception))
            {
                return null;
            }

            if (!this._genericExceptions.ContainsKey(initialType))
            {
                return this.GetRegisteredType(initialType.BaseType ?? typeof(Exception));
            }

            return initialType;
        }
    }
}
