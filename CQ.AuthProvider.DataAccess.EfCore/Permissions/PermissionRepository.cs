using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.UnitOfWork.EfCore.Core;
using CQ.UnitOfWork.EfCore.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CQ.AuthProvider.DataAccess.EfCore.Permissions;

internal sealed class PermissionRepository(
    AuthDbContext context,
    [FromKeyedServices(MapperKeyedService.DataAccess)] IMapper mapper)
    : EfCoreRepository<PermissionEfCore>(context),
    IPermissionRepository
{
    public async Task<Pagination<Permission>> GetAllAsync(
        Guid? appId,
        bool? isPrivate,
        Guid? roleId,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var appLoggedIsAuthWebApi = accountLogged.AppLogged.Id == AuthConstants.AUTH_WEB_API_APP_ID;

        var query = Entities
            .Where(p => (appLoggedIsAuthWebApi && p.AppId == AuthConstants.AUTH_WEB_API_APP_ID) || p.TenantId == accountLogged.Tenant.Id)
            .Where(p => isPrivate == null || p.IsPublic == !isPrivate)
            .Where(p => roleId == null || p.Roles.Any(r => r.Id == roleId))
            .Where(p => appId == null || p.AppId == appId);

        var permissions = await query
            .ToPaginateAsync(page, pageSize)
            .ConfigureAwait(false);

        return mapper.Map<Pagination<Permission>>(permissions);
    }

    public async Task<List<Permission>> GetAllByKeysAsync(
        List<(Guid appId, string key)> keys,
        AccountLogged accountLogged)
    {
        var keyesMapped = JsonConvert.SerializeObject(keys);

        // Define raw SQL query
        var sql = @"
        SELECT p.*
        FROM Permissions AS p
        INNER JOIN OPENJSON(@keyAppJson) 
        WITH (
            Item1 UNIQUEIDENTIFIER,
            Item2 NVARCHAR(MAX)
        ) AS j
        ON [p].[AppId] = [j].[Item1] AND [p].[Key] = [j].[Item2]
        WHERE p.TenantId = @tenantId AND @keyAppJson IS NOT NULL AND LEN(@keyAppJson) > 2";

        // Execute the raw query
        var permissions = await Entities
            .FromSqlRaw(sql,
                new SqlParameter("@keyAppJson", keyesMapped),
                new SqlParameter("@tenantId", accountLogged.Tenant.Id))
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
