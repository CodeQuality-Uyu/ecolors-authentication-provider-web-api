using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Accounts.Mappings;
using CQ.Exceptions;
using CQ.UnitOfWork.EfCore;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQ.AuthProvider.DataAccess.Accounts
{
    internal sealed class AccountEfCoreRepository : EfCoreRepository<AccountEfCore>
    {

        public AccountEfCoreRepository(EfCoreContext efCoreContext) : base(efCoreContext)
        {
        }

        public override async Task<AccountEfCore?> GetOrDefaultAsync(Expression<Func<AccountEfCore, bool>> predicate)
        {
            var query = _dbSet
                .Include(a => a.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(predicate);

            var account = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            return account;
        }

        public override async Task<AccountEfCore> GetByPropAsync(string value, string prop)
        {
            var account = await base.GetOrDefaultByPropAsync(value, prop).ConfigureAwait(false);

            if (Guard.IsNull(account))
                throw new SpecificResourceNotFoundException<AccountInfo>(prop, value);

            return account;
        }
    }
}
