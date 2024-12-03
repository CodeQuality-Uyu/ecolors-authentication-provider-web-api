using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.UnitOfWork.EfCore.Core;
using CQ.UnitOfWork.EfCore.Extensions;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.EfCore.Roles;

internal sealed class RoleRepository(
    AuthDbContext context,
    [FromKeyedServices(MapperKeyedService.DataAccess)] IMapper mapper,
    IRepository<PermissionEfCore> permissionRepository,
    IRepository<RolePermission> rolePermissionRepository)
    : EfCoreRepository<RoleEfCore>(context),
    IRoleRepository
{
    public async Task<Pagination<Role>> GetAllAsync(
        Guid? appId,
        bool? isPrivate,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var query = Entities
            .Where(r => r.TenantId == accountLogged.Tenant.Id)
            .Where(r => isPrivate == null || r.IsPublic == !isPrivate)
            .Where(r => appId == null || r.AppId == appId)
            .Paginate(page, pageSize);

        var roles = await query
            .ToPaginateAsync(page, pageSize)
            .ConfigureAwait(false);

        return mapper.Map<Pagination<Role>>(roles);
    }

    public async Task RemoveDefaultsAndSaveAsync(
        List<Guid> appsIds,
        AccountLogged accountLogged)
    {
        var query = Entities
            .Where(r => r.TenantId == accountLogged.Tenant.Id)
            .Where(r => appsIds.Contains(r.AppId))
            .Where(r => r.IsDefault);

        var roles = await query
            .ToListAsync()
            .ConfigureAwait(false);

        roles.ForEach(a =>
        {
            a.IsDefault = false;
        });

        Entities.UpdateRange(roles);
        await BaseContext
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }

    public new async Task<Role> GetByIdAsync(Guid id)
    {
        var query = Entities
           .Include(r => r.Permissions)
           .Where(r => r.Id == id);

        var role = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        Guard.ThrowIsNull(role, nameof(role));

        return mapper.Map<Role>(role);
    }

    public async Task<Role> GetDefaultByTenantIdAsync(Guid tenantId)
    {
        var query = Entities
            .Where(r => r.TenantId == tenantId)
            .Where(r => r.IsDefault);

        var role = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        Guard.ThrowIsNull(role, nameof(role));

        return mapper.Map<Role>(role);
    }

    public async Task CreateBulkAsync(List<Role> roles)
    {
        var rolesEfCore = roles.ConvertAll(r => new RoleEfCore(r));

        await CreateBulkAndSaveAsync(rolesEfCore)
            .ConfigureAwait(false);
    }

    public async Task AddPermissionsAsync(
        Guid id,
        List<string> permissionsKeys)
    {
        var permissions = await permissionRepository
            .GetAllAsync(p => permissionsKeys.Contains(p.Key))
            .ConfigureAwait(false);

        var rolePermissions = permissions.ConvertAll(p => new RolePermission
        {
            RoleId = id,
            PermissionId = p.Id
        });

        await rolePermissionRepository
            .CreateBulkAndSaveAsync(rolePermissions)
            .ConfigureAwait(false);
    }

    public async Task<Role?> GetDefaultOrDefaultByAppIdAndTenantIdAsync(
        Guid appId,
        Guid tenantId)
    {
        var query = Entities
            .Where(r => r.AppId == appId)
            .Where(r => r.TenantId == tenantId)
            .Where(r => r.IsDefault);

        var entity = await query
            .AsNoTracking()
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        return mapper.Map<Role>(entity);
    }
}
