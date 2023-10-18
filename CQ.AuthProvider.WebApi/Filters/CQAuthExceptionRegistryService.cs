using CQ.ApiElements.Filters;
using CQ.AuthProvider.BusinessLogic;
using System.Net;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal sealed class CQAuthExceptionRegistryService : ExceptionRegistryService
    {
        protected override void RegisterBusinessExceptions(ExceptionStoreService exceptionStoreService)
        {
            exceptionStoreService.RegisterException(
                new DinamicExceptionMapping<DuplicatedEmailException>(
   (exception, context) =>
                    {
                        return $"Email '{exception.Email}' is duplicated";

                    },
   "DuplicatedEmail",
   HttpStatusCode.Conflict));


            exceptionStoreService.RegisterException(new StaticExceptionMapping<InvalidCredentialsException>("Email or password incorrect", "InvalidCredentials", HttpStatusCode.BadRequest));
            exceptionStoreService.RegisterException(new StaticExceptionMapping<AuthDisabledException>("Auth of email is disabled", "AuthDisabled", HttpStatusCode.Conflict));
        }
    }
}
