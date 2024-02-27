using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.ResetPasswords.Exceptions;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords
{
    internal sealed class ResetPasswordService : IResetPasswordService
    {
        private readonly IAccountService _authService;
        private readonly IIdentityProviderRepository _identityProviderRepository;
        private readonly IRepository<ResetPasswordApplication> _resetPasswordRepository;

        public ResetPasswordService(
            IAccountService authService,
            IRepository<ResetPasswordApplication> resetPasswordRepository,
            IIdentityProviderRepository identityProviderRepository)
        {
            this._authService = authService;
            this._resetPasswordRepository = resetPasswordRepository;
            this._identityProviderRepository = identityProviderRepository;
        }

        public async Task AcceptAsync(string id, ResetPasswordApplicationAccepted request)
        {
            var resetPasswordOldApplication = await this._resetPasswordRepository.GetByIdAsync(id, new SpecificResourceNotFoundException<ResetPasswordApplicationAccepted>(nameof(ResetPasswordApplication.Id), id)).ConfigureAwait(false);

            if (resetPasswordOldApplication.Code != request.Code) throw new CodesNotMatchException(request.Code, resetPasswordOldApplication.Account.Email);

            await this._identityProviderRepository.UpdatePasswordAsync(resetPasswordOldApplication.Account.Id, request.NewPassword).ConfigureAwait(false);

            await this.DeleteByIdAsync(resetPasswordOldApplication.Id).ConfigureAwait(false);
        }

        private async Task DeleteByIdAsync(string id)
        {
            await this._resetPasswordRepository.DeleteAsync(r => r.Id == id).ConfigureAwait(false);
        }

        public async Task<ResetPasswordApplication> CreateAsync(string email)
        {
            var account = await this._authService.GetByEmailAsync(email).ConfigureAwait(false);

            var resetPasswordOldApplication = await this._resetPasswordRepository
                .GetOrDefaultByPropAsync(
                email, 
                $"{nameof(Account)}.{nameof(Account.Email)}")
                .ConfigureAwait(false);

            if (Guard.IsNotNull(resetPasswordOldApplication) && !resetPasswordOldApplication.HasExpired()) throw new DuplicateResetPasswordApplicationException(email);

            var resetPasswordApplication = resetPasswordOldApplication;

            if (Guard.IsNull(resetPasswordOldApplication))
            {
                resetPasswordApplication = await CreateResetPasswordApplicationAsync(account).ConfigureAwait(false);
            }
            else
            {
                await UpdateCodeOfResetPasswordApplicationAsync(resetPasswordOldApplication).ConfigureAwait(false);
            }

            //TODO: send email

            return resetPasswordApplication;
        }

        private async Task UpdateCodeOfResetPasswordApplicationAsync(ResetPasswordApplication resetPasswordOldApplication)
        {
            resetPasswordOldApplication.Code = ResetPasswordApplication.NewCode();
            resetPasswordOldApplication.CreatedAt = DateTimeOffset.UtcNow;

            var updates = new
            {
                resetPasswordOldApplication.Code,
                resetPasswordOldApplication.CreatedAt
            };
            await this._resetPasswordRepository.UpdateByIdAsync(resetPasswordOldApplication.Id, updates).ConfigureAwait(false);
        }

        private async Task<ResetPasswordApplication> CreateResetPasswordApplicationAsync(Account account)
        {
            ResetPasswordApplication? resetPasswordApplication = new ResetPasswordApplication(new MiniAccount(account.Id, account.Email));
            await this._resetPasswordRepository.CreateAsync(resetPasswordApplication).ConfigureAwait(false);
            return resetPasswordApplication;
        }

        public async Task<ResetPasswordApplication> GetActiveByIdAsync(string id)
        {
            var resetPasswordApplication = await this._resetPasswordRepository.GetByIdAsync(id, new SpecificResourceNotFoundException<ResetPasswordApplication>(nameof(ResetPasswordApplication.Id), id)).ConfigureAwait(false);

            if (resetPasswordApplication.HasExpired())
            {
                await this.DeleteByIdAsync(id).ConfigureAwait(false);

                throw new SpecificResourceNotFoundException<ResetPasswordApplication>(nameof(ResetPasswordApplication.Id), id);
            }

            return resetPasswordApplication;
        }
    }
}
