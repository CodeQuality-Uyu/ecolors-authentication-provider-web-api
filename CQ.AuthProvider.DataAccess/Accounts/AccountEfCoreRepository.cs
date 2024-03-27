using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Accounts.Mappings;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.DataAccess.Contexts;
using CQ.Exceptions;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.EfCore;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQ.AuthProvider.DataAccess.Accounts
{
    internal sealed class AccountEfCoreRepository : EfCoreRepository<AccountEfCore>, IAccountInfoRepository
    {
        private readonly IRepository<AccountRole> _accountRoleRepository;

        private readonly IMapper _mapper;

        public AccountEfCoreRepository(EfCoreContext efCoreContext) : base(efCoreContext)
        {
            this._accountRoleRepository = new EfCoreRepository<AccountRole>(efCoreContext);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountProfile>();
            });

            this._mapper = new Mapper(configuration);
        }

        public override async Task<AccountEfCore> CreateAsync(AccountEfCore entity)
        {
            var roleId = entity.Roles.First().Id;
            var accountRole = new AccountRole(entity.Id, roleId);

            entity.Roles = new List<RoleEfCore>();

            var accountCreated = await base.CreateAsync(entity).ConfigureAwait(false);

            await this._accountRoleRepository.CreateAsync(accountRole).ConfigureAwait(false);

            return accountCreated;
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
                throw new SpecificResourceNotFoundException<Account>(prop, value);

            return account;
        }

        public async Task<Account> GetInfoByIdAsync(string id)
        {
            var account = await base.GetByIdAsync(id).ConfigureAwait(false);

            return this._mapper.Map<Account>(account);
        }
    }
}
