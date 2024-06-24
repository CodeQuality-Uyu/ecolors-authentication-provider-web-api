using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.DataAccess.Mongo.Authorizations;
using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.DataAccess.Mongo.Accounts
{
    internal sealed class AccountMongoService : AccountService
    {
        private readonly IRepository<AccountMongo> _accountRepository;

        public AccountMongoService(
            IIdentityProviderRepository identityProviderRepository,
            ISessionInternalService sessionService,
            IRepository<AccountMongo> accountRepository,
            IRoleInternalService<RoleMongo> roleService,
            IMapper mapper)
            : base(
                  identityProviderRepository,
                  sessionService,
                  mapper,
                  roleService)
        {
            _accountRepository = accountRepository;
        }

        protected override async Task<Account> CreateAsync(CreateAccountArgs newAccount, Role role, Identity identity)
        {
            var miniRole = new MiniRoleMongo(role.Key, role.Permissions);

            var account = new AccountMongo(
                newAccount.FullName,
                newAccount.FirstName,
                newAccount.LastName,
                newAccount.Email,
                miniRole,
                newAccount.ProfilePictureUrl)
            {
                Id = identity.Id
            };

            await _accountRepository.CreateAsync(account).ConfigureAwait(false);

            return this._mapper.Map<Account>(account);
        }

        protected override async Task<bool> ExistByEmailAsync(string email)
        {
            var existAccount = await _accountRepository.ExistAsync(a => a.Email == email).ConfigureAwait(false);

            return existAccount;
        }

        public override async Task<Account> GetByEmailAsync(string email)
        {
            var account = await _accountRepository.GetByPropAsync(email, nameof(AccountMongo.Email)).ConfigureAwait(false);

            return this._mapper.Map<Account>(account);
        }

        protected override async Task<Account> GetByIdAsync(string id)
        {
            var account = await _accountRepository.GetByIdAsync(id).ConfigureAwait(false);

            return this._mapper.Map<Account>(account);
        }
    }
}
