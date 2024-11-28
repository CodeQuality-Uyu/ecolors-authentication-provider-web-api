using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Tenants;

public interface ITenantService
{
    Task CreateAsync(
        CreateTenantArgs args,
        AccountLogged accountLogged);

    Task<Pagination<Tenant>> GetAllAsync(
        int page = 1,
        int pageSize = 10);

    Task<Tenant> GetByIdAsync(
        Guid id,
        AccountLogged accountLogged);

    Task UpdateNameByIdAndSaveAsync(
        Guid id,
        string newName);

    Task UpdateOwnerAsync(
        Guid id,
        Guid newOwnerId,
        AccountLogged accountLogged);
}

internal interface ITenantInternalService
    : ITenantService
{
}


