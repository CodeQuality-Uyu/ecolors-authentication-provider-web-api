using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.UnitOfWork.EfCore;
using CQ.UnitOfWork.EfCore.Extensions;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess
{
    internal sealed class AccountEfCoreRepository : EfCoreRepository<Account>
    {
        public AccountEfCoreRepository(EfCoreContext efCoreContext) : base(efCoreContext)
        {
        }

        public override async Task<Account> GetByIdAsync<TException>(string id, TException exception)
        {
            var query = base._dbSet
                .Include(a => a.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(a => a.Id == id);

            var account = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            if(account == null)
            {
                throw exception;
            }

            return account;
        }
    }
}
