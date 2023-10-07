using CQ.ApiElements.Filters;
using CQ.AuthProvider.BusinessLogic;
using System.Net;

namespace CQ.AuthProvider.WebApi.Filters
{
    internal sealed class CQAuthExceptionRegistryService : ExceptionRegistryService
    {
        protected override void RegisterBusinessExceptions(ExceptionStoreService exceptionStoreService)
        {
            exceptionStoreService.RegisterMapping<DuplicatedEmailException>(
                (exception, context) =>
            {
                var customException = exception as DuplicatedEmailException;

                if (customException != null) 
                {
                    return "Email is duplicated";
                }

                return $"Email '{customException.Email}' is duplicated";
            },
                (exception, context) =>new
                {
                    Source = context.ControllerName,
                    Email = exception.Email
                },
                HttpStatusCode.Conflict,
                (exception, context) =>
                {
                    var customException = exception as DuplicatedEmailException;

                    if (customException != null)
                    {
                        return "Email is duplicated";
                    }

                    return $"Email '{customException.Email}' is duplicated";
                },
                "DuplicatedEmail");
        }
    }
}
