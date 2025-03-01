using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Roles;

public interface IRoleRepository
{
    Task<Pagination<Role>> GetAllAsync(
        Guid? appId,
        bool? isPrivate,
        int page,
        int pageSize,
        AccountLogged accountLogged);

    Task RemoveDefaultsAndSaveAsync(
        List<Guid> appsIds,
        AccountLogged accountLogged);

    Task CreateBulkAsync(List<Role> roles);

    Task<Role> GetByIdAsync(Guid id);

    Task AddPermissionsAsync(
        Guid id,
        List<string> permissionsKeys);

    Task<Role> GetDefaultByTenantIdAsync(
        Guid appId,
        Guid tenantId);

    Task<Role?> GetDefaultOrDefaultByAppIdAndTenantIdAsync(
        Guid appId,
        Guid tenantId);

    Task<List<Role>> GetByIdAsync(
        List<Guid> ids,
        List<Guid> appIds,
        Guid tenantId);

    Task<List<Role>> GetAllByIdsAsync(
        List<Guid> ids,
        AccountLogged accountLogged);

    Task DeleteAndSaveByIdAsync(Guid id);
}
