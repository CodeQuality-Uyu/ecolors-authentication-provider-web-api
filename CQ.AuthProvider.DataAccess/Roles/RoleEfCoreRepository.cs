using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Authorizations.Mappings;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.AuthProvider.DataAccess.Contexts;
using CQ.Exceptions;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.EfCore;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace CQ.AuthProvider.DataAccess.Roles
{
    internal sealed class RoleEfCoreRepository : EfCoreRepository<RoleEfCore>
    {
        private readonly IRepository<RolePermission> _rolePermissionRepository;

        public RoleEfCoreRepository(EfCoreContext efCoreContext)
            : base(efCoreContext)
        {
            this._rolePermissionRepository = new EfCoreRepository<RolePermission>(efCoreContext);
        }

        public async override Task<RoleEfCore> CreateAsync(RoleEfCore entity)
        {
            var rolePermissions = entity.Permissions.Select(p => new RolePermission(entity.Id, p.Id)).ToList();

            entity.Permissions = new List<PermissionEfCore>();

            var newRole = await base.CreateAsync(entity).ConfigureAwait(false);

            await this._rolePermissionRepository.CreateBulkAsync(rolePermissions);

            return newRole;
        }

        public override async Task<RoleEfCore?> GetOrDefaultAsync(Expression<Func<RoleEfCore, bool>> predicate)
        {
            var role = await base._dbSet
                .Include(r => r.Permissions)
                .Where(predicate)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return role;
        }

        public override async Task<RoleEfCore> GetByPropAsync(string value, string prop)
        {
            var role = await base.GetOrDefaultByPropAsync(value, prop).ConfigureAwait(false);

            if (Guard.IsNull(role))
                throw new SpecificResourceNotFoundException<RoleInfo>(prop, value);

            return role;
        }
    }
}
