using CQ.AuthProvider.BusinessLogic.Accounts;

namespace CQ.AuthProvider.BusinessLogic.Roles;

public interface IRoleRepository
{
    Task<List<Role>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? tenantId,
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
