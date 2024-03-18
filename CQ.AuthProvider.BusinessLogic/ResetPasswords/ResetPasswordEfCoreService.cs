using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords
{
    internal sealed class ResetPasswordEfCoreService : ResetPasswordService
    {
        private readonly IRepository<ResetPasswordApplicationEfCore> _resetPasswordApplicationRepository;
        public ResetPasswordEfCoreService(
            IAccountService accountService,
            IIdentityProviderRepository identityProviderRepository,
            IRepository<ResetPasswordApplicationEfCore> resetPasswordApplicationRepository,
            IMapper mapper)
            : base(
                  accountService,
                  identityProviderRepository,
                  mapper)
        {
            this._resetPasswordApplicationRepository = resetPasswordApplicationRepository;
        }

        #region Create
        protected override async Task<ResetPasswordApplication> CreateAsync(AccountInfo account)
        {
            var resetPasswordApplication = new ResetPasswordApplicationEfCore(account.Id);

            await this._resetPasswordApplicationRepository.CreateAsync(resetPasswordApplication).ConfigureAwait(false);

            return this._mapper.Map<ResetPasswordApplication>(resetPasswordApplication);
        }
        #endregion

        #region DeleteById
        protected override async Task DeleteByIdAsync(string id)
        {
            await this._resetPasswordApplicationRepository.DeleteAsync(r => r.Id == id).ConfigureAwait(false);
        }
        #endregion

        #region GetByid
        protected override async Task<object> GetByIdAsync(string id)
        {
            var resetPasswordApplication = await this._resetPasswordApplicationRepository.GetByIdAsync(id).ConfigureAwait(false);

            return resetPasswordApplication;
        }
        #endregion

        #region Create
        protected override async Task<object?> GetOrDefaultByAccountEmailAsync(string email)
        {
            var resetPassword = await this._resetPasswordApplicationRepository.GetOrDefaultByPropAsync(email, $"{nameof(ResetPasswordApplication.Account)}.{nameof(ResetPasswordApplication.Account.Email)}").ConfigureAwait(false);

            return resetPassword;
        }

        protected override async Task UpdateByIdAsync(string id, object updates)
        {
            await this._resetPasswordApplicationRepository.UpdateByIdAsync(id, updates).ConfigureAwait(false);
        }
        #endregion
    }
}
