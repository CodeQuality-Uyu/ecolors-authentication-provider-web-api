using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    internal sealed class AccountEfCoreService : AccountService
    {
        private readonly IRepository<AccountEfCore> _accountRepository;

        public AccountEfCoreService(
            IIdentityProviderRepository identityProviderRepository,
            ISessionInternalService sessionService,
            IRepository<AccountEfCore> accountRepository,
            IRoleInternalService<RoleEfCore> roleService,
            IMapper mapper)
            : base(identityProviderRepository,
                  sessionService,
                  mapper,
                  roleService)
        {
            this._accountRepository = accountRepository;
        }

        protected override async Task<Account> CreateAsync(
            CreateAccount newAccount,
            Role role,
            Identity identity)
        {
            var account = new AccountEfCore(
                newAccount.Email,
                newAccount.FullName,
                newAccount.FirstName,
                newAccount.LastName,
                role.Id)
            {
                Id = identity.Id
            };

            await this._accountRepository.CreateAsync(account).ConfigureAwait(false);

            return this._mapper.Map<Account>(account);
        }

        protected override async Task<bool> ExistByEmailAsync(string email)
        {
            var existAccount = await this._accountRepository.ExistAsync(a => a.Email == email).ConfigureAwait(false);

            return existAccount;
        }

        public override async Task<Account> GetByEmailAsync(string email)
        {
            var account = await this._accountRepository.GetByPropAsync(email, nameof(AccountEfCore.Email)).ConfigureAwait(false);

            return this._mapper.Map<Account>(account);
        }

        protected override async Task<Account> GetByIdAsync(string id)
        {
            var account = await this._accountRepository.GetByIdAsync(id).ConfigureAwait(false);

            return this._mapper.Map<Account>(account);
        }
    }
}
