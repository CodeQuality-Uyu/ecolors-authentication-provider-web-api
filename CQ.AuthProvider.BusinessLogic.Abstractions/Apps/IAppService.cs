
namespace CQ.AuthProvider.BusinessLogic.Abstractions.Apps;

public interface IAppService
{
}

internal interface IAppInternalService
    : IAppService
{
    Task<App> GetByIdAsync(string id);

    Task<List<App>> GetAllByIdAsync(List<string> ids);
}
