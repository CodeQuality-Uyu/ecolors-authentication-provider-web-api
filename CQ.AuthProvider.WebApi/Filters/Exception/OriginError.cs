namespace CQ.AuthProvider.WebApi.Filters
{
    public sealed record class OriginError(string ControllerName, string Action);
}
