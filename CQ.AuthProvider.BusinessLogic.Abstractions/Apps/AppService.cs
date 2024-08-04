using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Apps;

internal sealed class AppService(
    IAppRepository appRepository)
    : IAppInternalService
{
    public async Task<List<App>> GetAllByIdAsync(
        List<string> ids,
        AccountLogged accountLogged)
    {
        var invalidIds = ids
            .Where(i => !Db.IsIdValid(i))
            .ToList();
        if (invalidIds.Count != 0)
        {
            throw new ArgumentException($"Invalid ids {string.Join(",", invalidIds)}");
        }

        var app = await appRepository
            .GetByIdsAsync(ids)
            .ConfigureAwait(false);

        return app;
    }

    public async Task<App> GetByIdAsync(
        string id,
        AccountLogged accountLogged)
    {
        Db.ThrowIsInvalidId(id, nameof(id));

        var isNotOfAccountLogged = !accountLogged.AppsIds.Contains(id);
        if (isNotOfAccountLogged)
        {
            throw new InvalidOperationException("App is not of account logged");
        }

        var app = await appRepository
            .GetByIdAsync(id)
            .ConfigureAwait(false);

        return app;
    }

    public Task<App> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<App> GetByIdAsync(string id, Tenant tenant)
    {
        throw new NotImplementedException();
    }
}
