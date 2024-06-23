using CQ.ApiElements.Filters;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles.Exceptions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions.Exceptions;
using CQ.Exceptions;
using System.Net;

namespace CQ.AuthProvider.WebApi.Filters.Exception
{
    internal sealed class CQAuthExceptionRegistryService : ExceptionRegistryService
    {
        protected override void RegisterBusinessExceptions(ExceptionStoreService exceptionStoreService)
        {
            #region Specific exceptions
            #region Role controller
            #region Create
            exceptionStoreService
                .AddOriginExceptions(new("Role", "Create"))
                .AddException<PermissionNotFoundException>(
                "ResourceNotFound",
                HttpStatusCode.Conflict,
                (exception, context) => $"The following permissions are incorrect '{string.Join(',', exception.PermissionKeys)}'"
                );
            #endregion
            #region Add permission
            exceptionStoreService
                .AddOriginExceptions(new("Role", "AddPermission"))
                .AddException<PermissionNotFoundException>(
                "ResourceNotFound",
                HttpStatusCode.Conflict,
                (exception, context) => $"The following permissions are incorrect '{string.Join(',', exception.PermissionKeys)}'"
                )
                .AddException<PermissionsDuplicatedException>(
                "PermissionsDuplicated",
                HttpStatusCode.Conflict,
                (exception, context) => $"The following permissions are duplicated '{string.Join(',', exception.Keys)}"
                );
            #endregion
            #endregion

            #region Auth controller
            exceptionStoreService
                .AddOriginExceptions(
                new("Auth", "CreateCredentials"))
                .AddException<ResourceDuplicatedException>(
                    "DuplicatedEmail",
                    HttpStatusCode.Conflict,
                    "Exist another account with email provided"
                    )
                .AddException<SpecificResourceNotFoundException<Role>>(
                "InvalidRole",
                HttpStatusCode.Conflict,
                (exception, context) => "The role provided does not exist"
                )
                .AddException<InvalidCredentialsException>(
                "InvalidSession",
                HttpStatusCode.InternalServerError,
                (exception, context) => $"Operation failed due to an error in creating a session"
                )
                .AddException<AuthDisabledException>(
                "InvalidSession",
                HttpStatusCode.InternalServerError,
                (exception, context) => $"Operation due to an error in creating a session"
                );
            #endregion
            #endregion

            #region Generic exceptions
            exceptionStoreService

                .AddGenericException<InvalidCredentialsException>(
                "InvalidCredentials",
                HttpStatusCode.BadRequest,
                (exception, context) => $"The credentials provided are incorrect"
                )

                .AddGenericException<AuthDisabledException>(
                "AccountDisabled",
                HttpStatusCode.BadRequest,
                (exception, context) => $"The account is disabled",
                (exception, context) => $"The account with '{exception.Email}' is disabled"
                );
            #endregion
        }

        private string Join(List<string> values, string separator = ", ")
        {
            return string.Join(separator, values);
        }
    }
}
