using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Authorizations.Mappings;
using CQ.Exceptions;
using CQ.UnitOfWork.MongoDriver;
using MongoDB.Driver;
using System.Data;

namespace CQ.AuthProvider.DataAccess.Roles
{
    internal sealed class RoleMongoRepository : MongoDriverRepository<RoleMongo>, IRoleRepository<RoleMongo>
    {
        private readonly IMapper _mapper;

        public RoleMongoRepository(MongoContext mongoContext) : base(mongoContext)
        {
            var config = new MapperConfiguration(conf => conf.AddProfile<RoleProfile>());
            this._mapper = config.CreateMapper();
        }

        public async Task AddPermissionsByIdAsync(string id, List<Permission> permissions)
        {
            var filter = Builders<RoleMongo>.Filter.Eq(r => r.Id, id);
            var update = Builders<RoleMongo>.Update.PushEach(r => r.Permissions, permissions.Select(p => p.Key).ToList());

            await _collection.UpdateOneAsync(filter, update).ConfigureAwait(false);
        }

        public async Task<bool> ExistByKeyAsync(RoleKey key)
        {
            return await base.ExistAsync(r => r.Key == key.ToString()).ConfigureAwait(false);
        }

        public async Task<List<RoleInfo>> GetAllInfoAsync(bool isPrivate, AccountInfo accountLogged)
        {
            if (isPrivate)
            {
                var hasPermission = accountLogged.Permissions.Any(p => p == PermissionKey.GetAllPrivateRoles || p == PermissionKey.Joker);
                if (!hasPermission)
                {
                    throw new AccessDeniedException(PermissionKey.GetAllPrivateRoles.ToString());
                }
            }

            var roles = await base.GetAllAsync(r => r.IsPublic != isPrivate).ConfigureAwait(false);

            return this._mapper.Map<List<RoleInfo>>(roles);
        }

        public async Task<bool> HasPermissionByKeysAsync(List<RoleKey> keys, PermissionKey permissionKey)
        {
            var basicKeys = keys.Select(k => k.ToString()).ToList();

            return await base.ExistAsync(r => basicKeys.Contains(r.Key) && r.Permissions.Contains(permissionKey.ToString())).ConfigureAwait(false);
        }

        public async Task<RoleInfo> GetInfoByIdAsync<TException>(string id, TException exception)
            where TException : Exception
        {
            var role = await base.GetByIdAsync(id, exception).ConfigureAwait(false);

            return this._mapper.Map<RoleInfo>(role);
        }
    }
}
