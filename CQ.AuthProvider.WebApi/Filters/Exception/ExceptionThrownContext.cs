namespace CQ.AuthProvider.WebApi.Filters
{
    public sealed record class ExceptionThrownContext(Exception Exception, string ControllerName, string Action);
}
