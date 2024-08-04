namespace CQ.AuthProvider.BusinessLogic.Abstractions.Apps;

internal interface IAppRepository
{
    Task<App> GetByIdAsync(string id);

    Task<List<App>> GetByIdsAsync(List<string> ids);
}
