using CQ.ApiElements.Filters.ExceptionFilter;

namespace CQ.AuthProvider.WebApi.Filters;

public sealed class CqExceptionFilter(
    ExceptionStoreService exceptionStoreService)
    : ExceptionFilter(exceptionStoreService)
{
    protected override void LogResponse(ErrorResponse response)
    {
        Console.WriteLine(response.Exception);
    }
}
