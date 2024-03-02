using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    internal abstract class AccountService<TAccount> : IAccountService
        where TAccount : class
    {
        private readonly IIdentityProviderRepository _identityProviderRepository;

        protected readonly IAccountRepository<TAccount> _accountRepository;

        protected readonly ISessionService _sessionService;

        public AccountService(
            IIdentityProviderRepository identityProviderRepository,
            ISessionService sessionService,
            IAccountRepository<TAccount> accountRepository)
        {
            this._identityProviderRepository = identityProviderRepository;
            this._sessionService = sessionService;
            this._accountRepository = accountRepository;
        }

        public async Task<CreateAccountResult> CreateAsync(CreateAccount newAccount)
        {
            await AssertEmailInUse(newAccount.Email).ConfigureAwait(false);

            var identity = await this.CreateIdentityAsync(newAccount).ConfigureAwait(false);

            var account = await this.SaveNewAccountAsync(newAccount, identity).ConfigureAwait(false);

            var session = await _sessionService.CreateAsync(new CreateSessionCredentials(newAccount.Email, newAccount.Password)).ConfigureAwait(false);

            return new CreateAccountResult(account.Id, account.Email, account.Name, session.Token);
        }

        private async Task AssertEmailInUse(string email)
        {
            var existAuth = await this._accountRepository.ExistByEmailAsync(email).ConfigureAwait(false);

            if (existAuth)
            {
                throw new SpecificResourceNotFoundException<AccountInfo>(nameof(AccountEfCore.Email), email);
            }
        }

        private async Task<Identity> CreateIdentityAsync(CreateAccount newAccount)
        {
            var identity = new Identity(
                newAccount.Email,
                newAccount.Password);

            await _identityProviderRepository.CreateAsync(identity).ConfigureAwait(false);

            return identity;
        }

        protected abstract Task<AccountInfo> SaveNewAccountAsync(CreateAccount newAccount, Identity identity);

        public async Task UpdatePasswordAsync(string newPassword, AccountEfCore userLogged)
        {
            Guard.ThrowIsInputInvalidPassword(newPassword, "newPassword");

            await _identityProviderRepository.UpdatePasswordAsync(userLogged.Id, newPassword).ConfigureAwait(false);
        }

        public async Task<AccountInfo> GetMeAsync(string token)
        {
            var session = await this._sessionService.GetByTokenAsync(token).ConfigureAwait(false);

            var account = await this._accountRepository.GetInfoByIdAsync(
                session.AccountId,
                new SpecificResourceNotFoundException<AccountInfo>(nameof(AccountInfo.Id), session.AccountId))
                .ConfigureAwait(false);

            return account;
        }

        public async Task<AccountInfo> GetByEmailAsync(string email)
        {
            var account = await this._accountRepository
                .GetInfoByEmailAsync(
                email,
                new SpecificResourceNotFoundException<AccountInfo>(nameof(AccountInfo.Email), email))
                .ConfigureAwait(false);

            return account;
        }
    }
}
