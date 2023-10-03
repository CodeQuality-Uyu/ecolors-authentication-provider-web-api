using FirebaseAdmin.Auth;
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
        //private readonly IAuthService _authService;
        //private readonly IRepository<ResetPasswordRequest> _resetPasswordsRepository;
        //private readonly Random _random;

        //public ResetPasswordService(IUnitOfWork unitOfWork, IAuthService authService)
        //{
        //    this._resetPasswordsRepository = unitOfWork.GetRepository<ResetPasswordRequest>();
        //    this._random = new Random();
        //    this._authService = authService;
        //}

        //public async Task AcceptAsync(string id, NewPasswordRequest request)
        //{
        //    this.AssertResetPasswordData(id, request);

        //    var resetPasswordRequest = await this.GetByIdAsync(id).ConfigureAwait(false);

        //    if (resetPasswordRequest.Code != request.Code && resetPasswordRequest.User.Email != request.Email)
        //    {
        //        throw new CodesNotMatchException();
        //    }

        //    await this._authService.UpdatePasswordAsync(request.NewPassword, new User { Id = resetPasswordRequest.User.Id}).ConfigureAwait(false);

        //    this.DeleteResetPasswordAsync(resetPasswordRequest.Id);
        //}

        //private void AssertResetPasswordData(string userId, NewPasswordRequest request)
        //{
        //    var missingUserId = string.IsNullOrEmpty(userId);
        //    var missingPassword = string.IsNullOrEmpty(request.NewPassword);
        //    var missingCode = string.IsNullOrEmpty(request.Code);
        //    var missingEmail = string.IsNullOrEmpty(request.Email);

        //    var missingProperty = missingUserId ? "userId" : missingPassword ? "password" : missingCode ? "code" : "email";

        //    if (missingUserId || missingPassword || missingCode || missingEmail)
        //    {
        //        throw new ArgumentNullException(missingProperty);
        //    }
        //}

        //private async Task<ResetPasswordRequest> GetByIdAsync(string id)
        //{
        //    var resetPassword = await this._resetPasswordsRepository.GetAsync(resetPassword => resetPassword.Id == id).ConfigureAwait(false);

        //    return resetPassword;
        //}

        //private async Task DeleteResetPasswordAsync(string id)
        //{
        //    await this._resetPasswordsRepository.DeleteAsync(resetPassword => resetPassword.Id == id).ConfigureAwait(false);
        //}

        //public async Task<ResetPasswordRequest> CreateAsync(string email)
        //{
        //    this.AssertUserEmail(email);

        //    var user = await this._userService.GetByEmailAsync(email).ConfigureAwait(false);

        //    if(user is null)
        //    {
        //        user = await this._authService.GetByEmailAsync(email).ConfigureAwait(false);

        //        this._userService.CreateAsync(user);
        //    }

        //    var resetPassword = new ResetPasswordRequest
        //    {
        //        Code = this._random.Next(111, 1000).ToString(),
        //        User = new MiniUser
        //        {
        //            Id = user.Id,
        //            Email = email
        //        }
        //    };

        //    var resetPasswordRequestCreated = await this._resetPasswordsRepository.CreateAsync(resetPassword).ConfigureAwait(false);

        //    return resetPasswordRequestCreated;
        //}

        //private void AssertUserEmail(string email)
        //{
        //    if (string.IsNullOrEmpty(email))
        //    {
        //        throw new ArgumentNullException(nameof(email));
        //    }
        //}

        //public async Task<ResetPasswordRequest> GetActiveResetPasswordByIdAsync(string id)
        //{
        //    var resetPassword = await this._resetPasswordsRepository.GetAsync(resetPassword => resetPassword.Id == id).ConfigureAwait(false);

        //    return resetPassword;
        //}
        public Task AcceptAsync(string id, NewPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResetPasswordRequest> CreateAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ResetPasswordRequest> GetActiveResetPasswordByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
