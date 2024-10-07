using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.UnitOfWork.EfCore.Core;
using CQ.UnitOfWork.EfCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions;

internal sealed class PermissionRepository(
    AuthDbContext context,
    IMapper mapper)
    : EfCoreRepository<PermissionEfCore>(context),
    IPermissionRepository
{
    public async Task<Pagination<Permission>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? roleId,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var appLoggedIsAuthWebApi = accountLogged.AppLogged.Id == AuthConstants.AUTH_WEB_API_APP_ID;

        var query = _entities
            .Where(p => (appLoggedIsAuthWebApi && p.AppId == AuthConstants.AUTH_WEB_API_APP_ID) || p.TenantId == accountLogged.TenantValue.Id)
            .Where(p => isPrivate == null || p.IsPublic == !isPrivate)
            .Where(p => roleId == null || p.Roles.Any(r => r.Id == roleId))
            .Where(p => appId == null || p.AppId == appId);

        var permissions = await query
            .PaginateAsync(null, page, pageSize)
            .ConfigureAwait(false);

        return mapper.Map<Pagination<Permission>>(permissions);
    }

    public async Task<List<Permission>> GetAllByKeysAsync(
        List<(string appId, List<string> keys)> keys,
        AccountLogged accountLogged)
    {
        var query = _entities
            .Where(p => keys.Any(i => i.appId == p.AppId && i.keys.Contains(p.Key)))
            .Where(p => p.TenantId == accountLogged.TenantValue.Id);

        var permissions = await query
            .ToListAsync()
            .ConfigureAwait(false);

        return mapper.Map<List<Permission>>(permissions);
    }

    async Task IPermissionRepository.CreateBulkAndSaveAsync(List<Permission> permissions)
    {
        var permissionsEfCore = permissions.ConvertAll(p => new PermissionEfCore(p));

        await CreateBulkAndSaveAsync(permissionsEfCore).ConfigureAwait(false);
    }
}
