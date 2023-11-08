using CQ.ApiElements.Filters;
using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Authorization.Exceptions;
using CQ.AuthProvider.BusinessLogic.Exceptions;
using System.Net;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal sealed class CQAuthExceptionRegistryService : ExceptionRegistryService
    {
        protected override void RegisterBusinessExceptions(ExceptionStoreService exceptionStoreService)
        {
            exceptionStoreService.RegisterException(new DinamicExceptionMapping<ResourceDuplicatedException>((exception, context) => $"Exist another resource with same '{exception.PropDuplicated}'", "DuplicatedResource", HttpStatusCode.Conflict));
            exceptionStoreService.RegisterException(new DinamicExceptionMapping<ResourceNotFoundException>((exception, context) => exception.Resource != null ? $"The specific resource '{exception.Resource}' was not found" : "An error has ocurred, try again later.", "ResourceNotFound", HttpStatusCode.NotFound));
            exceptionStoreService.RegisterException(new DinamicExceptionMapping<PermissionNotFoundException>((exception, context) => $"The following permissions were not found: {string.Join(',', exception.PermissionKeys)}", "ResourceNotFound", HttpStatusCode.BadRequest));

            exceptionStoreService.RegisterException(new StaticExceptionMapping<InvalidCredentialsException>("Email or password incorrect", "InvalidCredentials", HttpStatusCode.BadRequest));
            exceptionStoreService.RegisterException(new StaticExceptionMapping<AuthDisabledException>("Auth of email is disabled", "AuthDisabled", HttpStatusCode.Conflict));
        }
    }
}
