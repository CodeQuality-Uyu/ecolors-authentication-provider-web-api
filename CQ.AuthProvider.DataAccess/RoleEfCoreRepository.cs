using CQ.AuthProvider.BusinessLogic;
using CQ.UnitOfWork.EfCore;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess
{
    internal sealed class RoleEfCoreRepository : EfCoreRepository<Role>, IRoleRepository
    {
        public RoleEfCoreRepository(EfCoreContext efCoreContext)
            : base(efCoreContext)
        {
        }

        public async Task AddPermissionsByIdAsync(string id, List<Permission> permissionsKeys)
        {
            await base._dbSet
                .Where(r => r.Id == id)
                .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    r => r.Permissions,
                    r => r.Permissions.Concat(permissionsKeys).ToList()))
                .ConfigureAwait(false);
        }
    }
}
