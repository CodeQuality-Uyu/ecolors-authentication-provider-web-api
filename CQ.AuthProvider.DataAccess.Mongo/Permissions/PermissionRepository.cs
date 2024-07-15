using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.UnitOfWork.MongoDriver.Core;

namespace CQ.AuthProvider.DataAccess.Mongo.Permissions;

internal sealed class PermissionRepository(
    AuthDbContext context,
    IMapper mapper)
    : MongoDriverRepository<PermissionMongo>(context),
    IPermissionRepository
{
    public async Task CreateAsync(Permission permission)
    {
        var permissionMongo = new PermissionMongo(
            permission.Id,
            permission.Name,
            permission.Description,
            permission.Key,
            permission.IsPublic);

        await CreateAsync(permissionMongo).ConfigureAwait(false);
    }

    public async Task CreateBulkAsync(List<Permission> permissions)
    {
        var permissionsMongo = permissions.ConvertAll(p => new PermissionMongo(
            p.Id,
            p.Name,
            p.Description,
            p.Key,
            p.IsPublic));

        await CreateBulkAsync(permissionsMongo).ConfigureAwait(false);
    }

    public async Task<bool> ExistByKeyAsync(PermissionKey permissionKey)
    {
        var key = permissionKey.ToString();

        var exist = await ExistAsync(p => p.Key == key).ConfigureAwait(false);

        return exist;
    }

    public async Task<List<Permission>> GetAllAsync(
        string? appId,
        bool? isPrivate,
        string? roleId,
        AccountLogged accountLogged)
    {
        var permissions = await
            GetAllAsync(p =>
            (isPrivate == null || p.IsPublic == !isPrivate))
            .ConfigureAwait(false);

        return mapper.Map<List<Permission>>(permissions);
    }

    public async Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> permissionsKeys)
    {
        var keys = mapper.Map<List<string>>(permissionsKeys);

        var permissions = await GetAllAsync(p => keys.Contains(p.Key));

        return mapper.Map<List<Permission>>(permissions);
    }
}
