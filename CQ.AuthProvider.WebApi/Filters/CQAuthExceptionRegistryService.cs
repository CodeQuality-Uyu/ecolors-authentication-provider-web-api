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
        }
    }
}
