using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    internal sealed class AccountMongoService : IAccountService
    {
        private readonly IIdentityProviderRepository _identityProviderRepository;

        private readonly IRepository<AccountMongo> _accountRepository;

        private readonly ISessionService _sessionService;

        private readonly IRoleInternalService _roleService;

        public AccountMongoService(
            IIdentityProviderRepository identityProviderRepository,
            ISessionService sessionService,
            IRepository<AccountMongo> accountRepository,
            IRoleInternalService roleService)
        {
            this._identityProviderRepository = identityProviderRepository;
            this._sessionService = sessionService;
            this._accountRepository = accountRepository;
            this._roleService = roleService;
        }

        private async Task<AccountInfo> SaveAuthAsync(CreateAccount newAuth)
        {
            var role = await this._roleService.GetByKeyAsync(newAuth.Role).ConfigureAwait(false);

            var identity = await base.CreateIdentityAsync(newAuth).ConfigureAwait(false);

            var miniRole = new MiniRole(role.Key, role.Permissions);

            var auth = new AccountMongo(
                newAuth.Email,
                newAuth.FullName(),
                miniRole)
            {
                Id = identity.Id
            };

            await this._accountRepository.CreateAsync(auth).ConfigureAwait(false);

            return auth;
        }

        public async Task<AccountInfo> GetMeAsync(string token)
        {
            var session = await this._sessionService.GetByTokenAsync(token).ConfigureAwait(false);

            var account = await this._accountRepository.GetByIdAsync(
                session.AccountId,
                new SpecificResourceNotFoundException<Session>(nameof(Session.AccountId), session.AccountId))
                .ConfigureAwait(false);

            var accountInfo = new AccountInfo(
                account.Id,
                account.Roles.Select(r => r.Name).ToList(),
                account.Roles.SelectMany(r => r.Permissions).ToList());

            return accountInfo;
        }

        public async Task<AccountInfo> GetByEmailAsync(string email)
        {
            var account = await this._accountRepository
                .GetByPropAsync(
                email,
                nameof(Account.Email),
                new SpecificResourceNotFoundException<Account>(nameof(Account.Email), email))
                .ConfigureAwait(false);

            var accountInfo = new AccountInfo(
                account.Id,
                account.Roles.Select(r => r.Name).ToList(),
                account.Roles.SelectMany(r => r.Permissions).ToList());

            return accountInfo;
        }
    }
}
