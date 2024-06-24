using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.Abstractions;

namespace CQ.AuthProvider.DataAccess.Mongo
{
    internal sealed class ResetPasswordMongoService : ResetPasswordService
    {
        private readonly IRepository<ResetPassword> _resetPasswordApplicationRepository;
        public ResetPasswordMongoService(
            IRepository<ResetPassword> resetPasswordApplicationRepository,
            IAccountService accountService,
            IMapper mapper,
            IIdentityProviderRepository identityProviderRepository)
            : base(
                  accountService,
                  identityProviderRepository,
                  mapper)
        {
            _resetPasswordApplicationRepository = resetPasswordApplicationRepository;
        }

        #region Create
        protected override async Task<ResetPassword> CreateAsync(Account account)
        {
            var resetPasswordApplication = new ResetPassword(new MiniAccount(account.Id, account.Email));

            await _resetPasswordApplicationRepository.CreateAsync(resetPasswordApplication).ConfigureAwait(false);

            return resetPasswordApplication;
        }
        #endregion

        #region DeleteById
        protected override async Task DeleteByIdAsync(string id)
        {
            await _resetPasswordApplicationRepository.DeleteAsync(r => r.Id == id).ConfigureAwait(false);
        }
        #endregion

        #region GetById
        protected override async Task<object> GetByIdAsync(string id)
        {
            var resetPasswordApplication = await _resetPasswordApplicationRepository.GetByIdAsync(id).ConfigureAwait(false);

            return resetPasswordApplication;
        }
        #endregion

        #region Create
        protected override async Task<object?> GetOrDefaultByAccountEmailAsync(string email)
        {
            var resetPassword = await _resetPasswordApplicationRepository.GetOrDefaultByPropAsync(email, $"{nameof(ResetPassword.Account)}.{nameof(ResetPassword.Account.Email)}").ConfigureAwait(false);

            return resetPassword;
        }

        protected override async Task UpdateByIdAsync(string id, object updates)
        {
            await _resetPasswordApplicationRepository.UpdateByIdAsync(id, updates).ConfigureAwait(false);
        }
        #endregion
    }
}
