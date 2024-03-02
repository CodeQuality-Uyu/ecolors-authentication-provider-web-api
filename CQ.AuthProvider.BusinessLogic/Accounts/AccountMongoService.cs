using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    internal sealed class AccountMongoService : AccountService<AccountMongo>
    {
        private readonly IRoleInternalService<RoleMongo> _roleService;

        private readonly IMapper _mapper;

        public AccountMongoService(
            IIdentityProviderRepository identityProviderRepository,
            ISessionService sessionService,
            IAccountRepository<AccountMongo> accountRepository,
            IRoleInternalService<RoleMongo> roleService,
            IMapper mapper)
            : base(
                  identityProviderRepository,
                  sessionService,
                  accountRepository)
        {
            this._roleService = roleService;
            this._mapper = mapper;
        }

        protected override async Task<AccountInfo> SaveNewAccountAsync(CreateAccount newAccount, Identity identity)
        {
            var role = await this._roleService.GetByKeyAsync(newAccount.Role).ConfigureAwait(false);

            var miniRole = new AccountRoleMongo(role.Key, role.Permissions);

            var account = new AccountMongo(
                newAccount.FullName(),
                newAccount.Email,
                miniRole)
            {
                Id = identity.Id
            };

            await this._accountRepository.CreateAsync(account).ConfigureAwait(false);

            return this._mapper.Map<AccountInfo>(account);
        }
    }
}
