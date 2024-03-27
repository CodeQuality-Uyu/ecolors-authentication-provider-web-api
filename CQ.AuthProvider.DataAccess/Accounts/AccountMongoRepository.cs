using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Accounts.Mappings;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.Exceptions;
using CQ.UnitOfWork.MongoDriver;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.Accounts
{
    internal sealed class AccountMongoRepository : MongoDriverRepository<AccountMongo>, IAccountInfoRepository
    {
        private readonly IMapper _mapper;

        public AccountMongoRepository(MongoContext mongoContext) : base(mongoContext)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountProfile>();
            });

            this._mapper = new Mapper(configuration);
        }

        public override async Task<AccountMongo> GetByPropAsync(string value, string prop)
        {
            var account = await base.GetOrDefaultByPropAsync(value, prop).ConfigureAwait(false);

            if (Guard.IsNull(account))
                throw new SpecificResourceNotFoundException<AccountMongo>(prop, value);

            return account;
        }

        public async Task<Account> GetInfoByIdAsync(string id)
        {
            var account = await base.GetByIdAsync(id).ConfigureAwait(false);

            return this._mapper.Map<Account>(account);
        }
    }
}
