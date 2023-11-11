namespace CQ.AuthProvider.WebApi.Filters
{
    internal sealed record class OriginError(string ControllerName, string Action);
}
