using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.Exceptions;
using CQ.UnitOfWork.EfCore;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQ.AuthProvider.DataAccess.ClientSystems
{
    internal sealed class ClientSystemEfCoreRepository : EfCoreRepository<ClientSystemEfCore>
    {
        public ClientSystemEfCoreRepository(EfCoreContext efCoreContext) : base(efCoreContext)
        {
        }

        public override async Task<ClientSystemEfCore?> GetOrDefaultAsync(Expression<Func<ClientSystemEfCore, bool>> predicate)
        {
            var clientSystem = await base._dbSet
                .Include(c => c.Role)
                    .ThenInclude(r => r.Permissions)
                .Where(predicate)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return clientSystem;
        }

        public override async Task<ClientSystemEfCore> GetByPropAsync(string value, string prop)
        {
            var clientSystem = await base.GetOrDefaultByPropAsync(value, prop).ConfigureAwait(false);

            if (Guard.IsNull(clientSystem))
                throw new SpecificResourceNotFoundException<ClientSystem>(prop, value);

            return clientSystem;
        }
    }
}
