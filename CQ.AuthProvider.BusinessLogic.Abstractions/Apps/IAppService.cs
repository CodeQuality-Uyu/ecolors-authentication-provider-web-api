
namespace CQ.AuthProvider.BusinessLogic.Abstractions.Apps;

public interface IAppService
{
}

internal interface IAppInternalService
    : IAppService
{
    Task<App> GetByIdAsync(string id);
}
