namespace CQ.AuthProvider.BusinessLogic.Apps;

public interface IAppService
{
}

internal interface IAppInternalService
    : IAppService
{
    Task<App> GetByIdAsync(string id);
}
