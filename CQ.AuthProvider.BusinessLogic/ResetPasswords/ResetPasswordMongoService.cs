using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.ResetPasswords
{
    internal sealed class ResetPasswordMongoService : ResetPasswordService<ResetPasswordApplication>
    {
        public ResetPasswordMongoService(
            IAccountService accountService,
            IResetPasswordApplicationRepository<ResetPasswordApplication> resetPasswordRepository,
            IIdentityProviderRepository identityProviderRepository) 
            : base(
                  accountService,
                  resetPasswordRepository,
                  identityProviderRepository)
        {
        }

        protected override async Task<ResetPasswordApplication> CreateResetPasswordApplicationAsync(AccountInfo account)
        {
            var resetPasswordApplication = new ResetPasswordApplication(new MiniAccount(account.Id, account.Email));

            await this._resetPasswordRepository.CreateAsync(resetPasswordApplication).ConfigureAwait(false);
         
            return resetPasswordApplication;
        }
    }
}
