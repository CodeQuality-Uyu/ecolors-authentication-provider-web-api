using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.UnitOfWork.Abstractions.Repositories;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Permissions;

internal sealed class PermissionService(
    IPermissionRepository permissionRepository,
    IAppRepository appRepository)
    : IPermissionInternalService
{
    public async Task<Pagination<Permission>> GetAllAsync(
        Guid? appId,
        bool? isPrivate,
        Guid? roleId,
        string? name,
        string? key,
        int page,
        int pageSize,
        AccountLogged accountLogged)
    {
        var permissions = await permissionRepository
            .GetAllAsync(
            appId,
            isPrivate,
            roleId,
            name,
            key,
            page,
            pageSize,
            accountLogged)
            .ConfigureAwait(false);

        return permissions;
    }

    public async Task CreateAsync(
        CreatePermissionArgs args,
        AccountLogged accountLogged)
    {
        await CreateBulkAsync(
            new CreateBulkPermissionArgs([args], args.AppId),
            accountLogged)
            .ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(
        CreateBulkPermissionArgs args,
        AccountLogged accountLogged)
    {
        var allPermissionsKeys = args
            .Permissions
            .ConvertAll(a => a.Key)
            .Distinct()
            .ToList();

        var duplicatedPermissions = await permissionRepository
            .GetAllByKeysAsync(
            args.AppId,
            allPermissionsKeys,
            accountLogged)
            .ConfigureAwait(false);
        if (duplicatedPermissions.Count != 0)
        {
            var permissionsSavedKeys = duplicatedPermissions
                .ConvertAll(p => (p.App.Id, p.Key));

            throw new InvalidOperationException($"Duplicated keys exist {string.Join(",", permissionsSavedKeys)}");
        }

        var app = accountLogged.Apps.First(a => a.Id == args.AppId);
        var permissions = args
            .Permissions
            .ConvertAll(p => new Permission(
            p.Name,
            p.Description,
            p.IsPublic,
            p.Key,
            app));

        await permissionRepository
            .CreateBulkAndSaveAsync(permissions)
            .ConfigureAwait(false);
    }

    public async Task UpdateAsync(
        Guid id,
        UpdatePermissionArgs args,
        AccountLogged accountLogged)
    {
        var normalized = args with
        {
            Name = Guard.Normalize(args.Name),
            Description = Guard.Normalize(args.Description)
        };

        await AssertUpdatableAsync(
            [(id, normalized.AppId, normalized.Key)],
            accountLogged)
            .ConfigureAwait(false);

        await permissionRepository
            .UpdateAndSaveByIdAsync(
            id,
            normalized,
            accountLogged)
            .ConfigureAwait(false);
    }

    public async Task UpdateBulkAsync(
        UpdateBulkPermissionArgs args,
        AccountLogged accountLogged)
    {
        var normalized = args
            .Permissions
            .ConvertAll(p => p with
            {
                Name = Guard.Normalize(p.Name),
                Description = Guard.Normalize(p.Description)
            });

        await AssertUpdatableAsync(
            normalized.ConvertAll(p => (p.Id, p.AppId, p.Key)),
            accountLogged)
            .ConfigureAwait(false);

        await permissionRepository
            .UpdateBulkAndSaveAsync(
            normalized,
            accountLogged)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Validates a set of permission updates before they are applied: no
    /// duplicate (appId, key) within the request, every target app belongs to
    /// the tenant, and no (appId, key) collides with a different existing
    /// permission.
    /// </summary>
    private async Task AssertUpdatableAsync(
        List<(Guid Id, Guid AppId, string Key)> items,
        AccountLogged accountLogged)
    {
        var pairs = items.ConvertAll(i => (i.AppId, i.Key));

        // 1. No duplicate (appId, key) within the request itself.
        if (pairs.Count != pairs.Distinct().Count())
        {
            throw new InvalidOperationException("Duplicated (appId, key) pairs within the request");
        }

        // 2. Every target app must exist in the caller's tenant.
        var appIds = items
            .ConvertAll(i => i.AppId)
            .Distinct()
            .ToList();

        var existingAppIds = await appRepository
            .GetExistingIdsInTenantAsync(appIds, accountLogged.Tenant.Id)
            .ConfigureAwait(false);

        var missingAppIds = appIds
            .Except(existingAppIds)
            .ToList();
        if (missingAppIds.Count != 0)
        {
            throw new InvalidOperationException($"Apps don't belong to the tenant: {string.Join(",", missingAppIds)}");
        }

        // 3. No (appId, key) collides with a different existing permission.
        var updatingIds = items.ConvertAll(i => i.Id);
        foreach (var group in items.GroupBy(i => i.AppId))
        {
            var keys = group
                .Select(i => i.Key)
                .Distinct()
                .ToList();

            var candidates = await permissionRepository
                .GetAllByKeysAsync(group.Key, keys, accountLogged)
                .ConfigureAwait(false);

            var collisions = candidates
                .Where(c => !updatingIds.Contains(c.Id))
                .ToList();
            if (collisions.Count != 0)
            {
                var collisionKeys = collisions.ConvertAll(c => (c.App.Id, c.Key));

                throw new InvalidOperationException($"Duplicated keys exist {string.Join(",", collisionKeys)}");
            }
        }
    }
}
