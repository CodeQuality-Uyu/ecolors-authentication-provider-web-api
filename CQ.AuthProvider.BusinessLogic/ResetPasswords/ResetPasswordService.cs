using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.ResetPasswords.Exceptions;
using CQ.Exceptions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords
{
    internal abstract class ResetPasswordService : IResetPasswordService
    {
        protected readonly IAccountService _accountService;
        protected readonly IIdentityProviderRepository _identityProviderRepository;
        protected readonly IMapper _mapper;

        public ResetPasswordService(
            IAccountService accountService,
            IIdentityProviderRepository identityProviderRepository,
            IMapper mapper)
        {
            this._accountService = accountService;
            this._identityProviderRepository = identityProviderRepository;
            this._mapper = mapper;
        }

        #region Accept
        public async Task AcceptAsync(string id, ResetPasswordApplicationAccepted request)
        {
            var resetPasswordOldApplication = await this.GetByIdAsync(id).ConfigureAwait(false);

            var resetPasswordOldApplicationMapped = this._mapper.Map<ResetPasswordApplication>(resetPasswordOldApplication);

            if (resetPasswordOldApplicationMapped.Code != request.Code)
                throw new CodesNotMatchException(
                    request.Code,
                    resetPasswordOldApplicationMapped.Account.Email);

            await this._identityProviderRepository
                .UpdatePasswordAsync(
                resetPasswordOldApplicationMapped.Account.Id,
                request.NewPassword)
                .ConfigureAwait(false);

            await this.DeleteByIdAsync(resetPasswordOldApplicationMapped.Id).ConfigureAwait(false);
        }

        protected abstract Task<object> GetByIdAsync(string id);
        #endregion

        #region DeleteById
        protected abstract Task DeleteByIdAsync(string id);
        #endregion

        #region Create
        async Task IResetPasswordService.CreateAsync(string email)
        {
            var resetPasswordOldApplication = await this
                .GetOrDefaultByAccountEmailAsync(email)
                .ConfigureAwait(false);

            string code;
            if (Guard.IsNull(resetPasswordOldApplication))
            {
                code = await this.CreateAsync(email).ConfigureAwait(false);
            }
            else
            {
                var resetPasswordOldApplicationMapped = this._mapper.Map<ResetPasswordApplication>(resetPasswordOldApplication);
                code = await this.UpdateCodeAsync(resetPasswordOldApplicationMapped).ConfigureAwait(false);
            }

            //TODO: send email
        }

        protected abstract Task<object?> GetOrDefaultByAccountEmailAsync(string email);

        private async Task<string> CreateAsync(string email)
        {
            var account = await this._accountService.GetByEmailAsync(email).ConfigureAwait(false);
            var resetPasswordApplication = await CreateAsync(account).ConfigureAwait(false);

            return resetPasswordApplication.Code;
        }

        private async Task<string> UpdateCodeAsync(ResetPasswordApplication resetPasswordOldApplication)
        {
            if (!resetPasswordOldApplication.HasExpired())
                throw new DuplicateResetPasswordApplicationException(resetPasswordOldApplication.Account.Email);

            var (newCode, createdAt) = await UpdateCodeByIdAsync(resetPasswordOldApplication.Id).ConfigureAwait(false);

            return newCode;
        }

        private async Task<(string newCode, DateTimeOffset createdAt)> UpdateCodeByIdAsync(string id)
        {
            var updates = new
            {
                Code = ResetPasswordApplication.NewCode(),
                CreatedAt = DateTimeOffset.UtcNow
            };

            await this.UpdateByIdAsync(id, updates).ConfigureAwait(false);

            return (updates.Code, updates.CreatedAt);
        }

        protected abstract Task UpdateByIdAsync(string id, object updates);

        protected abstract Task<ResetPasswordApplication> CreateAsync(Account account);
        #endregion

        #region GetActiveById
        public async Task<ResetPasswordApplication> GetActiveByIdAsync(string id)
        {
            var resetPasswordApplication = await this
                .GetByIdAsync(id)
                .ConfigureAwait(false);
            var resetPasswordApplicationMapped = this._mapper.Map<ResetPasswordApplication>(resetPasswordApplication);

            if (resetPasswordApplicationMapped.HasExpired())
            {
                await this.DeleteByIdAsync(id).ConfigureAwait(false);

                throw new SpecificResourceNotFoundException<ResetPasswordApplication>(nameof(ResetPasswordApplication.Id), id);
            }

            return resetPasswordApplicationMapped;
        }
        #endregion
    }
}
