using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    internal sealed class AccountEfCoreService : AccountService<AccountEfCore>
    {
        private readonly IRoleInternalService<RoleEfCore> _roleService;

        private readonly IMapper _mapper;

        public AccountEfCoreService(
            IIdentityProviderRepository identityProviderRepository,
            ISessionService sessionService,
            IAccountRepository<AccountEfCore> accountRepository,
            IRoleInternalService<RoleEfCore> roleService,
            IMapper mapper)
            : base(identityProviderRepository,
                  sessionService,
                  accountRepository)
        {
            this._roleService = roleService;
            this._mapper = mapper;
        }

        protected override async Task<AccountInfo> SaveNewAccountAsync(CreateAccount newAccount, Identity identity)
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

            await base._accountRepository.CreateAsync(account).ConfigureAwait(false);

            return this._mapper.Map<AccountInfo>(account);
        }
    }
}
