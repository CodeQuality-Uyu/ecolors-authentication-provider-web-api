using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Authorizations.Mappings;
using CQ.AuthProvider.DataAccess.Roles;
using CQ.UnitOfWork.MongoDriver;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.Permissions
{
    internal sealed class PermissionMongoRepository : MongoDriverRepository<Permission>, IPermissionRepository<Permission>
    {
        private readonly IMapper _mapper;

        private readonly RoleMongoRepository _roleMongoRepository;

        public PermissionMongoRepository(MongoContext mongoContext) : base(mongoContext)
        {
            var config = new MapperConfiguration(conf => conf.AddProfile<PermissionProfile>());
            this._mapper = config.CreateMapper();
            this._roleMongoRepository = new RoleMongoRepository(mongoContext);
        }

        public async Task<bool> ExistByKeyAsync(PermissionKey permissionKey)
        {
            return await base.ExistAsync(p => p.Key == permissionKey.ToString()).ConfigureAwait(false);
        }

        public async Task<List<Permission>> GetAllByKeysAsync(List<PermissionKey> permissionKeys)
        {
            var basicPermissions = this._mapper.Map<List<string>>(permissionKeys);

            var permissions = await base.GetAllAsync<Permission>(p => basicPermissions.Contains(p.Key)).ConfigureAwait(false);

            return permissions;
        }

        public async Task<List<Permission>> GetAllInfoAsync(bool isPrivate, string? roleId, AccountInfo accountLogged)
        {
            if (isPrivate)
                accountLogged.AssertPermission(PermissionKey.GetAllPrivateRoles);


            var permissionsToGet = new List<string>();
            if (Guard.IsNotNullOrEmpty(roleId))
            {
                accountLogged.AssertPermission(PermissionKey.GetAllPermissionsByRoleId);
                
                var role = await this._roleMongoRepository.GetByIdAsync(roleId).ConfigureAwait(false);

                permissionsToGet = role.Permissions;
            }

            return await base.GetAllAsync<Permission>(p =>
            p.IsPublic != isPrivate &&
            (string.IsNullOrEmpty(roleId) || permissionsToGet.Contains(p.Id)))
                .ConfigureAwait(false);
        }
    }
}
