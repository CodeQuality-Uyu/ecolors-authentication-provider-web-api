namespace CQ.AuthProvider.BusinessLogic.Abstractions.Apps;

public interface IAppRepository
{
    Task<App> GetByIdAsync(string id);

    Task<List<App>> GetByIdsAsync(List<string> ids);
}
