using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.Tenants;

public interface ITenantService
{
    Task CreateAsync(
        CreateTenantArgs args,
        AccountLogged accountLogged);

    Task<Pagination<Tenant>> GetAllAsync(
        int page = 1,
        int pageSize = 10);

    Task<Tenant> GetByIdAsync(
        string id,
        AccountLogged accountLogged);
}

internal interface ITenantInternalService
    : ITenantService
{
}


