using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Apps;

public interface IAppService
{
    Task CreateAsync(
        CreateAppArgs args,
        AccountLogged accountLogged);

    Task<Pagination<App>> GetAllAsync(
        int page,
        int pageSize,
        AccountLogged accountLogged);

    Task<App> GetByIdAsync(Guid id);

    Task UpdateColorsByIdAsync(
        Guid id,
        CreateAppCoverBackgroundColorArgs args,
        AccountLogged accountLogged);
}

internal interface IAppInternalService
    : IAppService
{
    Task<List<App>> GetByIdAsync(List<Guid> ids);
}
