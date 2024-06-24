using CQ.ApiElements.Filters;

namespace CQ.AuthProvider.WebApi.Filters;

public sealed class CqExceptionFilter(
    ExceptionStoreService exceptionStoreService)
    : ExceptionFilter(exceptionStoreService)
{
    protected override void LogResponse(ExceptionResponse response)
    {
        Console.WriteLine(response.Context.Exception);
    }
}
