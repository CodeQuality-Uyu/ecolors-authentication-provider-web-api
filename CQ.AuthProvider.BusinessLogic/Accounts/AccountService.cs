using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.Exceptions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Accounts
{
    internal abstract class AccountService : IAccountService
    {
        private readonly IIdentityProviderRepository _identityProviderRepository;

        protected readonly ISessionInternalService _sessionService;

        protected readonly IMapper _mapper;

        protected readonly IRoleInternalService _roleInternalService;

        public AccountService(
            IIdentityProviderRepository identityProviderRepository,
            ISessionInternalService sessionService,
            IMapper mapper,
            IRoleInternalService roleInternalService)
        {
            this._identityProviderRepository = identityProviderRepository;
            this._sessionService = sessionService;
            this._mapper = mapper;
            this._roleInternalService = roleInternalService;
        }

        #region Create
        public async Task<CreateAccountResult> CreateAsync(CreateAccount newAccount)
        {
            await AssertEmailInUseAsync(newAccount.Email).ConfigureAwait(false);

            var identity = await this.CreateIdentityAsync(newAccount).ConfigureAwait(false);

            try
            {
                Role role;
                if (Guard.IsNull(newAccount.Role))
                    role = await this._roleInternalService.GetDefaultAsync().ConfigureAwait(false);
                else
                    role = await this._roleInternalService.GetByKeyAsync(newAccount.Role).ConfigureAwait(false);

                var account = await this.CreateAsync(newAccount, role, identity).ConfigureAwait(false);

                var session = await _sessionService.CreateAsync(identity).ConfigureAwait(false);

                var accountToReturn = new CreateAccountResult(
                    account.Id,
                    account.Email,
                    account.FullName,
                    account.FirstName,
                    account.LastName,
                    session.Token,
                    account.Roles,
                    account.Permissions);

                return accountToReturn;
            }
            catch (SpecificResourceNotFoundException<Role>)
            {
                await this._identityProviderRepository.DeleteByIdAsync(identity.Id).ConfigureAwait(false);
                throw;
            }
        }

        private async Task AssertEmailInUseAsync(string email)
        {
            var existAuth = await this.ExistByEmailAsync(email).ConfigureAwait(false);

            if (existAuth)
                throw new SpecificResourceDuplicatedException<Account>(nameof(Account.Email), email);
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

        protected abstract Task<Account> CreateAsync(CreateAccount newAccount, Role role, Identity identity);
        #endregion

        public async Task UpdatePasswordAsync(string newPassword, AccountEfCore userLogged)
        {
            Guard.ThrowIsInputInvalidPassword(newPassword, "newPassword");

            await _identityProviderRepository.UpdatePasswordAsync(userLogged.Id, newPassword).ConfigureAwait(false);
        }

        #region GetMe
        public async Task<Account> GetByTokenAsync(string token)
        {
            var session = await this._sessionService.GetByTokenAsync(token).ConfigureAwait(false);

            var account = await this
                .GetByIdAsync(session.AccountId)
                .ConfigureAwait(false);

            return account;
        }

        protected abstract Task<Account> GetByIdAsync(string id);
        #endregion

        public abstract Task<Account> GetByEmailAsync(string email);
    }
}
