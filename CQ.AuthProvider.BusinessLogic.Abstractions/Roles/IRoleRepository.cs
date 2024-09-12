using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.Abstractions.Repositories;

namespace CQ.AuthProvider.BusinessLogic.Roles;

public interface IRoleRepository
{
    Task<Pagination<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        int page,
        int pageSize,
        AccountLogged accountLogged);

    Task RemoveDefaultsAndSaveAsync(
        List<string> appsIds,
        AccountLogged accountLogged);

    Task CreateBulkAsync(List<Role> roles);

    Task<Role> GetByIdAsync(string id);

    Task AddPermissionsAsync(
        string id,
        List<string> permissionsKeys);

    Task<Role> GetDefaultAsync();
}
