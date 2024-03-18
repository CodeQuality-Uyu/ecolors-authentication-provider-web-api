using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.Exceptions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    internal abstract class AccountService : IAccountService
    {
        private readonly IIdentityProviderRepository _identityProviderRepository;

        protected readonly ISessionService _sessionService;

        protected readonly IMapper _mapper;

        public AccountService(
            IIdentityProviderRepository identityProviderRepository,
            ISessionService sessionService,
            IMapper mapper)
        {
            this._identityProviderRepository = identityProviderRepository;
            this._sessionService = sessionService;
            this._mapper = mapper;
        }

        #region Create
        public async Task<CreateAccountResult> CreateAsync(CreateAccount newAccount)
        {
            await AssertEmailInUseAsync(newAccount.Email).ConfigureAwait(false);

            var identity = await this.CreateIdentityAsync(newAccount).ConfigureAwait(false);

            try
            {
                var account = await this.CreateAsync(newAccount, identity).ConfigureAwait(false);

                var session = await _sessionService.CreateAsync(new CreateSessionCredentials(newAccount.Email, newAccount.Password)).ConfigureAwait(false);

                return new CreateAccountResult(
                    account.Id,
                    account.Email,
                    account.FullName,
                    account.FirstName,
                    account.LastName,
                    session.Token,
                    account.Roles,
                    account.Permissions);
            }
            catch (SpecificResourceNotFoundException<RoleInfo>) 
            {
                await this._identityProviderRepository.DeleteByIdAsync(identity.Id).ConfigureAwait(false);
                throw;
            }
        }

        private async Task AssertEmailInUseAsync(string email)
        {
            var existAuth = await this.ExistByEmailAsync(email).ConfigureAwait(false);

            if (existAuth)
                throw new SpecificResourceNotFoundException<AccountInfo>(nameof(AccountEfCore.Email), email);
        }

        protected abstract Task<bool> ExistByEmailAsync(string email);

        private async Task<Identity> CreateIdentityAsync(CreateAccount newAccount)
        {
            var identity = new Identity(
                newAccount.Email,
                newAccount.Password);

            await _identityProviderRepository.CreateAsync(identity).ConfigureAwait(false);

            return identity;
        }

        protected abstract Task<AccountInfo> CreateAsync(CreateAccount newAccount, Identity identity);
        #endregion

        public async Task UpdatePasswordAsync(string newPassword, AccountEfCore userLogged)
        {
            Guard.ThrowIsInputInvalidPassword(newPassword, "newPassword");

            await _identityProviderRepository.UpdatePasswordAsync(userLogged.Id, newPassword).ConfigureAwait(false);
        }

        #region GetMe
        public async Task<AccountInfo> GetMeAsync(string token)
        {
            var session = await this._sessionService.GetByTokenAsync(token).ConfigureAwait(false);

            var account = await this
                .GetByIdAsync(session.AccountId)
                .ConfigureAwait(false);

            return account;
        }

        protected abstract Task<AccountInfo> GetByIdAsync(string id);
        #endregion

        public abstract Task<AccountInfo> GetByEmailAsync(string email);
    }
}
