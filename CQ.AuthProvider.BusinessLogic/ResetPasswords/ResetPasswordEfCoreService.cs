using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.ResetPasswords.Exceptions;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords
{
    internal sealed class ResetPasswordEfCoreService : ResetPasswordService<ResetPasswordApplicationEfCore>
    {
        private readonly IMapper _mapper;
        public ResetPasswordEfCoreService(
            IAccountService accountService,
            IResetPasswordApplicationRepository<ResetPasswordApplicationEfCore> resetPasswordRepository,
            IIdentityProviderRepository identityProviderRepository,
            IMapper mapper)
            : base(
                  accountService,
                  resetPasswordRepository,
                  identityProviderRepository)
        {
            this._mapper = mapper;
        }

        protected override async Task<ResetPasswordApplication> CreateResetPasswordApplicationAsync(AccountInfo account)
        {
            var resetPasswordApplication = new ResetPasswordApplicationEfCore(account.Id);

            await this._resetPasswordRepository.CreateAsync(resetPasswordApplication).ConfigureAwait(false);

            return this._mapper.Map<ResetPasswordApplication>(resetPasswordApplication);
        }
    }
}
