using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Accounts.Mappings;
using CQ.UnitOfWork.EfCore;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.Accounts
{
    internal sealed class AccountEfCoreRepository : EfCoreRepository<AccountEfCore>, IAccountRepository<AccountEfCore>
    {
        private readonly IMapper _mapper;

        public AccountEfCoreRepository(EfCoreContext efCoreContext) : base(efCoreContext)
        {
            var config = new MapperConfiguration(conf => conf.AddProfile<AccountProfile>());
            this._mapper = config.CreateMapper();
        }

        public async Task<bool> ExistByEmailAsync(string email)
        {
            return await base.ExistAsync(a => a.Email == email).ConfigureAwait(false);
        }

        public override async Task<AccountEfCore> GetByIdAsync<TException>(string id, TException exception)
        {
            var account = await this.GetOrDefaultByIdAsync(id).ConfigureAwait(false);

            if (account == null)
                throw exception;

            return account;
        }

        public override async Task<AccountEfCore?> GetOrDefaultByIdAsync(string id)
        {
            var query = _dbSet
                .Include(a => a.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(a => a.Id == id);

            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public override async Task<AccountEfCore> GetByIdAsync(string id)
        {
            var account = await this.GetOrDefaultByIdAsync(id).ConfigureAwait(false);

            if (Guard.IsNull(account))
                throw new InvalidOperationException($"{base.EntityName} not found");

            return account;
        }

        public async Task<AccountInfo> GetInfoByIdAsync<TException>(string id, TException exception)
            where TException : Exception
        {
            var account = await this.GetByIdAsync(id, exception).ConfigureAwait(false);

            return this._mapper.Map<AccountInfo>(account);
        }

        public async Task<AccountInfo> GetInfoByIdAsync(string id)
        {
            var account = await this.GetByIdAsync(id).ConfigureAwait(false);

            return this._mapper.Map<AccountInfo>(account);
        }

        public async Task<AccountInfo> GetInfoByEmailAsync<TException>(string email, TException exception)
            where TException : Exception
        {
            var query = _dbSet
                .Include(a => a.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(a => a.Email == email);

            var account = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            if (Guard.IsNull(account))
                throw exception;

            return this._mapper.Map<AccountInfo>(account);
        }
    }
}
