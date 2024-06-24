
using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions;

internal sealed class PermissionRepository(
    AuthDbContext context,
    IMapper mapper)
    : EfCoreRepository<PermissionEfCore>(context),
    IPermissionRepository
{
    public async Task<List<Permission>> GetAllAsync(
        bool? isPrivate,
        string? roleId,
        Account accountLogged)
    {
        var query = _dbSet
            .Where(p => isPrivate == null || p.IsPublic == !isPrivate)
            .Where(p => roleId == null || p.Roles.Exists(r => r.RoleId == roleId));

        var permissions = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Permission>>(permissions);
    }

    public async Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> permissionKeys)
    {
        var keys = mapper.Map<List<string>>(permissionKeys);

        var query = _dbSet
            .Where(p => keys.Contains(p.Key));

        var permissions = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Permission>>(permissions);
    }

    public async Task<bool> ExistByKeyAsync(PermissionKey permissionKey)
    {
        var key = permissionKey.ToString();

        var exist = await ExistAsync(p => p.Key == key).ConfigureAwait(false);

        return exist;
    }

    public async Task CreateAsync(Permission permission)
    {
        var permissionEfCore = mapper.Map<PermissionEfCore>(permission);

        await CreateAsync(permissionEfCore).ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(List<Permission> permissions)
    {
        var permissionsEfCore = mapper.Map<List<PermissionEfCore>>(permissions);

        await CreateBulkAsync(permissionsEfCore).ConfigureAwait(false);
    }
}
