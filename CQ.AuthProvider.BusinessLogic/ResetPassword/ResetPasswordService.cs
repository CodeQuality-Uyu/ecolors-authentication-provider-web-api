using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;
using PlayerFinder.Auth.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    internal class ResetPasswordService : IResetPasswordService
    {
        private readonly IAuthService _authService;
        private readonly IIdentityProviderRepository _identityProviderRepository;
        private readonly IRepository<ResetPasswordApplication> _resetPasswordRepository;

        public ResetPasswordService(IAuthService authService, IRepository<ResetPasswordApplication> resetPasswordRepository, IIdentityProviderRepository identityProviderRepository)
        {
            this._authService = authService;
            this._resetPasswordRepository = resetPasswordRepository;
            _identityProviderRepository = identityProviderRepository;
        }

        //public async Task<ResetPasswordRequest> GetActiveResetPasswordByIdAsync(string id)
        //{
        //    var resetPassword = await this._resetPasswordsRepository.GetAsync(resetPassword => resetPassword.Id == id).ConfigureAwait(false);

        //    return resetPassword;
        //}

        public async Task AcceptAsync(string id, ResetPasswordApplicationAccepted request)
        {
            var resetPasswordOldApplication = await this._resetPasswordRepository.GetByPropAsync(id, new SpecificResourceNotFoundException<ResetPasswordApplicationAccepted>(nameof(ResetPasswordApplication.Id), id)).ConfigureAwait(false);

            if (resetPasswordOldApplication.Code != request.Code && resetPasswordOldApplication.Auth.Email == request.Email) throw new CodesNotMatchException(request.Code, request.Email);

            await this._identityProviderRepository.UpdatePasswordAsync(resetPasswordOldApplication.Auth.Id, request.NewPassword).ConfigureAwait(false);

            await this.DeleteByIdAsync(resetPasswordOldApplication.Id).ConfigureAwait(false);
        }

        private async Task DeleteByIdAsync(string id)
        {
            await this._resetPasswordRepository.DeleteAsync(r => r.Id == id).ConfigureAwait(false);
        }

        public async Task<ResetPasswordApplication> CreateAsync(string email)
        {
            var auth = await this._authService.GetByEmailAsync(email).ConfigureAwait(false);

            var resetPasswordOldApplication = await this._resetPasswordRepository.GetOrDefaultByPropAsync(email, nameof(Auth.Email)).ConfigureAwait(false);

            if (Guard.IsNotNull(resetPasswordOldApplication) && !resetPasswordOldApplication.HasExpired()) throw new DuplicateResetPasswordApplicationException(email);

            var resetPasswordApplication = new ResetPasswordApplication(new MiniAuth(auth.Id, auth.Email));

            if (Guard.IsNull(resetPasswordOldApplication))
            {
                await this._resetPasswordRepository.CreateAsync(resetPasswordApplication).ConfigureAwait(false);
            }
            else
            {
                await this._resetPasswordRepository.UpdateByIdAsync(resetPasswordOldApplication.Id, resetPasswordOldApplication).ConfigureAwait(false);
            }

            //TODO: send email

            return resetPasswordApplication;
        }

        public async Task<ResetPasswordApplication> GetActiveByIdAsync(string id)
        {
            var resetPasswordApplication = await this._resetPasswordRepository.GetByPropAsync(id, new SpecificResourceNotFoundException<ResetPasswordApplication>(nameof(ResetPasswordApplication.Id), id)).ConfigureAwait(false);

            if (resetPasswordApplication.HasExpired())
            {
                await this.DeleteByIdAsync(id).ConfigureAwait(false);

                throw new SpecificResourceNotFoundException<ResetPasswordApplication>(nameof(ResetPasswordApplication.Id), id);
            }

            return resetPasswordApplication;
        }
    }
}
