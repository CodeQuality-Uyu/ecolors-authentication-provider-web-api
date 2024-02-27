using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    internal sealed class AccountService : IAccountService
    {
        private readonly IIdentityProviderRepository _identityProviderRepository;

        private readonly IRepository<Account> _accountRepository;

        private readonly ISessionService _sessionService;

        private readonly IRoleInternalService _roleService;

        public AccountService(
            IIdentityProviderRepository identityProviderRepository,
            ISessionService sessionService,
            IRepository<Account> accountRepository,
            IRoleInternalService roleService)
        {
            this._identityProviderRepository = identityProviderRepository;
            this._sessionService = sessionService;
            this._accountRepository = accountRepository;
            this._roleService = roleService;
        }

        public async Task<CreateAccountResult> CreateAsync(CreateAccount newAuth)
        {
            await AssertEmailInUse(newAuth.Email).ConfigureAwait(false);

            var role = await this._roleService.GetByKeyAsync(newAuth.Role).ConfigureAwait(false);

            var auth = await SaveAuthAsync(newAuth, role).ConfigureAwait(false);

            var session = await _sessionService.CreateAsync(new CreateSessionCredentials(newAuth.Email, newAuth.Password)).ConfigureAwait(false);

            return new CreateAccountResult(auth.Id, auth.Email, auth.Name, session.Token);
        }

        private async Task AssertEmailInUse(string email)
        {
            var existAuth = await this._accountRepository.ExistAsync(a => a.Email == email).ConfigureAwait(false);

            if (existAuth)
            {
                throw new ResourceDuplicatedException(nameof(Account.Email), email, nameof(Account));
            }
        }

        private async Task<Account> SaveAuthAsync(CreateAccount newAuth, Role role)
        {
            var identity = new Identity(
                newAuth.Email,
                newAuth.Password);

            await _identityProviderRepository.CreateAsync(identity).ConfigureAwait(false);

            var auth = new Account(
                newAuth.Email,
                newAuth.FullName(),
                role)
            {
                Id = identity.Id
            };

            await this._accountRepository.CreateAsync(auth).ConfigureAwait(false);

            return auth;
        }

        public async Task UpdatePasswordAsync(string newPassword, Account userLogged)
        {
            Guard.ThrowIsInputInvalidPassword(newPassword, "newPassword");

            await _identityProviderRepository.UpdatePasswordAsync(userLogged.Id, newPassword).ConfigureAwait(false);
        }

        public async Task<Account> GetMeAsync(string token)
        {
            var session = await this._sessionService.GetByTokenAsync(token).ConfigureAwait(false);

            var account = await this._accountRepository.GetByIdAsync(
                session.AccountId,
                new SpecificResourceNotFoundException<Session>(nameof(Session.AccountId), session.AccountId))
                .ConfigureAwait(false);

            return account;
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            var account = await this._accountRepository
                .GetByPropAsync(
                email,
                nameof(Account.Email),
                new SpecificResourceNotFoundException<Account>(nameof(Account.Email), email))
                .ConfigureAwait(false);
            
            return account;
        }

        public async Task<bool> HasPermissionAsync(string permission, Account userlogged)
        {
            return await this._roleService.HasPermissionAsync(userlogged.ConcreteRoles(), permission).ConfigureAwait(false);
        }
    }
}
