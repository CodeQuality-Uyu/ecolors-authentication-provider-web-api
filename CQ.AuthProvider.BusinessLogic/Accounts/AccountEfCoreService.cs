using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    internal sealed class AccountEfCoreService : AccountService
    {
        private readonly IRoleInternalService<RoleEfCore> _roleService;
        private readonly IRepository<AccountEfCore> _accountRepository;

        public AccountEfCoreService(
            IIdentityProviderRepository identityProviderRepository,
            ISessionService sessionService,
            IRepository<AccountEfCore> accountRepository,
            IRoleInternalService<RoleEfCore> roleService,
            IMapper mapper)
            : base(identityProviderRepository,
                  sessionService,
                  mapper)
        {
            this._roleService = roleService;
            this._accountRepository = accountRepository;
        }

        protected override async Task<AccountInfo> CreateAsync(CreateAccount newAccount, Identity identity)
        {
            var role = await this._roleService.GetByKeyAsync(newAccount.Role).ConfigureAwait(false);

            var account = new AccountEfCore(
                newAccount.Email,
                newAccount.FullName,
                newAccount.FirstName,
                newAccount.LastName,
                role)
            {
                Id = identity.Id
            };

            await this._accountRepository.CreateAsync(account).ConfigureAwait(false);

            return this._mapper.Map<AccountInfo>(account);
        }

        protected override async Task<bool> ExistByEmailAsync(string email)
        {
            var existAccount = await this._accountRepository.ExistAsync(a => a.Email == email).ConfigureAwait(false);

            return existAccount;
        }

        public override async Task<AccountInfo> GetByEmailAsync(string email)
        {
            var account = await this._accountRepository.GetByPropAsync(email, nameof(AccountEfCore.Email)).ConfigureAwait(false);

            return this._mapper.Map<AccountInfo>(account);
        }

        protected override async Task<AccountInfo> GetByIdAsync(string id)
        {
            var account = await this._accountRepository.GetByIdAsync(id).ConfigureAwait(false);

            return this._mapper.Map<AccountInfo>(account);
        }
    }
}
