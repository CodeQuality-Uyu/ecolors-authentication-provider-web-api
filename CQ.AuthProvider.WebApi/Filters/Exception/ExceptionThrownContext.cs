namespace CQ.AuthProvider.WebApi.Filters
{
    internal sealed record class ExceptionThrownContext(Exception Exception, string ControllerName, string Action);
}
