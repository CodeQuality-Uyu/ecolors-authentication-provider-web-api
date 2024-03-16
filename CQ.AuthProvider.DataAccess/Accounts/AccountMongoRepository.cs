using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Accounts.Mappings;
using CQ.UnitOfWork.MongoDriver;

namespace CQ.AuthProvider.DataAccess.Accounts
{
    internal sealed class AccountMongoRepository : MongoDriverRepository<AccountMongo>, IAccountRepository<AccountMongo>
    {
        private readonly IMapper _mapper;

        public AccountMongoRepository(MongoContext mongoContext) : base(mongoContext)
        {
            var config = new MapperConfiguration(conf => conf.AddProfile<AccountProfile>());
            this._mapper = config.CreateMapper();
        }

        public async Task<bool> ExistByEmailAsync(string email)
        {
            return await base.ExistAsync(a => a.Email == email).ConfigureAwait(false);
        }

        public async Task<AccountInfo> GetInfoByIdAsync<TException>(string id, TException exception)
            where TException : Exception
        {
            var account = await base.GetByIdAsync(id, exception).ConfigureAwait(false);

            return this._mapper.Map<AccountInfo>(account);
        }
        public async Task<AccountInfo> GetInfoByIdAsync(string id)
        {
            var account = await base.GetByIdAsync(id).ConfigureAwait(false);

            return this._mapper.Map<AccountInfo>(account);
        }

        public async Task<AccountInfo> GetInfoByEmailAsync<TException>(string email, TException exception)
            where TException : Exception
        {
            var account = await base.GetByPropAsync(email, nameof(AccountMongo.Email), exception).ConfigureAwait(false);

            return this._mapper.Map<AccountInfo>(account);
        }
    }
}
