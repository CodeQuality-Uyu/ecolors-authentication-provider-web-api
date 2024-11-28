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
}

internal interface IAppInternalService
    : IAppService
{
    Task<App> GetByIdAsync(Guid id);
}
