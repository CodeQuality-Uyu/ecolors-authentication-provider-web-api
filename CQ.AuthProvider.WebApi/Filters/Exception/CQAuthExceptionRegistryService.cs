
using CQ.ApiElements.Filters;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Authorization.Exceptions;
using CQ.AuthProvider.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace CQ.AuthProvider.WebApi.Filters
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
                .AddException<InvalidCredentialsException>(
                "InvalidOperation",
                HttpStatusCode.Conflict,
                (exception, context) => $"The creation of the account was interrupted",
                (exception, context) => $"The account with '{exception.Email}' was not found"
                )
                .AddException<AuthDisabledException>(
                "InvalidOperation",
                HttpStatusCode.Conflict,
                (exception, context) => $"The creation of the account was interrupted",
                (exception, context) => $"The account with '{exception.Email}' is disabled"
                )
                .AddException<SpecificResourceNotFoundException<Role>>(
                "InvalidOperation",
                HttpStatusCode.Conflict,
                (exception, context) => "The role specified does not exist");
            #endregion
            #endregion

            #region Generic exceptions
            exceptionStoreService

                .AddGenericException<InvalidRequestException>(
                    "InvalidRequest",
                    HttpStatusCode.BadRequest,
                    (exception, context) => $"The prop '{exception.Prop}' has the following error '{exception.Error}'")

                .AddGenericException<ResourceNotFoundException>(
                    "ResourceNotFound",
                    HttpStatusCode.NotFound,
                    (exception, context) => $"The resource with '{exception.Key}': '{exception.Value}' was not found",
                    (exception, context) => $"The resource '{exception.Resource}' with '{exception.Key}': '{exception.Value}' not found")

                .AddGenericException<ResourceDuplicatedException>(
                    "ResourceDuplicated",
                    HttpStatusCode.Conflict,
                    (exception, context) => $"Exist a resource with '{exception.Key}': '{exception.Value}'",
                    (exception, context) => $"Exist a resource '{exception.Resource}' with '{exception.Key}': '{exception.Value}'")

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
    }
}
