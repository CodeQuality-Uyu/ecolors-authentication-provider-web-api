using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.ResetPasswords.Exceptions;
using CQ.Exceptions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords
{
    internal abstract class ResetPasswordService<TResetPasswordApplication> : IResetPasswordService
        where TResetPasswordApplication : class
    {
        private readonly IAccountService _accountService;
        private readonly IIdentityProviderRepository _identityProviderRepository;
        protected readonly IResetPasswordApplicationRepository<TResetPasswordApplication> _resetPasswordRepository;


        public ResetPasswordService(
            IAccountService accountService,
            IResetPasswordApplicationRepository<TResetPasswordApplication> resetPasswordRepository,
            IIdentityProviderRepository identityProviderRepository)
        {
            this._accountService = accountService;
            this._resetPasswordRepository = resetPasswordRepository;
            this._identityProviderRepository = identityProviderRepository;
        }

        public async Task AcceptAsync(string id, ResetPasswordApplicationAccepted request)
        {
            var resetPasswordOldApplication = await this._resetPasswordRepository.GetInfoByIdAsync(
                id,
                new SpecificResourceNotFoundException<ResetPasswordApplicationAccepted>(
                    nameof(ResetPasswordApplication.Id),
                    id))
                .ConfigureAwait(false);

            if (resetPasswordOldApplication.Code != request.Code) throw new CodesNotMatchException(
                request.Code,
                resetPasswordOldApplication.Account.Email);

            await this._identityProviderRepository
                .UpdatePasswordAsync(
                resetPasswordOldApplication.Account.Id,
                request.NewPassword)
                .ConfigureAwait(false);

            await this.DeleteByIdAsync(resetPasswordOldApplication.Id).ConfigureAwait(false);
        }

        private async Task DeleteByIdAsync(string id)
        {
            await this._resetPasswordRepository.DeleteByIdAsync(id).ConfigureAwait(false);
        }

        public async Task CreateAsync(string email)
        {
            var account = await this._accountService.GetByEmailAsync(email).ConfigureAwait(false);

            var resetPasswordOldApplication = await this._resetPasswordRepository
                .GetOrDefaultInfoByAccountEmailAsync(email)
                .ConfigureAwait(false);

            if (Guard.IsNotNull(resetPasswordOldApplication) && !resetPasswordOldApplication.HasExpired())
                throw new DuplicateResetPasswordApplicationException(email);

            var resetPasswordApplication = resetPasswordOldApplication;

            if (Guard.IsNull(resetPasswordOldApplication))
            {
                resetPasswordApplication = await CreateResetPasswordApplicationAsync(account).ConfigureAwait(false);
            }
            else
            {
                var (newCode, createdAt) = await UpdateCodeByIdAsync(resetPasswordOldApplication.Id).ConfigureAwait(false);

                resetPasswordApplication.Code = newCode;
                resetPasswordApplication.CreatedAt = createdAt;
            }

            //TODO: send email
        }

        protected abstract Task<ResetPasswordApplication> CreateResetPasswordApplicationAsync(AccountInfo account);

        private async Task<(string newCode, DateTimeOffset createdAt)> UpdateCodeByIdAsync(string id)
        {
            var updates = new
            {
                Code = ResetPasswordApplication.NewCode(),
                CreatedAt = DateTimeOffset.UtcNow
            };

            await this._resetPasswordRepository.UpdateByIdAsync(id, updates).ConfigureAwait(false);

            return (updates.Code, updates.CreatedAt);
        }

        public async Task<ResetPasswordApplication> GetActiveByIdAsync(string id)
        {
            var resetPasswordApplication = await this._resetPasswordRepository
                .GetInfoByIdAsync(
                id,
                new SpecificResourceNotFoundException<ResetPasswordApplication>(
                    nameof(ResetPasswordApplication.Id),
                    id))
                .ConfigureAwait(false);

            if (resetPasswordApplication.HasExpired())
            {
                await this.DeleteByIdAsync(id).ConfigureAwait(false);

                throw new SpecificResourceNotFoundException<ResetPasswordApplication>(nameof(ResetPasswordApplication.Id), id);
            }

            return resetPasswordApplication;
        }
    }
}
