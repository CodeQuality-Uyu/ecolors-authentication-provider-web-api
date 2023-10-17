using CQ.Utility;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;
using PlayerFinder.Auth.Core.Exceptions;
using System.Net.Http.Json;
using System.Security.Authentication;

namespace CQ.AuthProvider.BusinessLogic
{
    internal class AuthFirebaseService : IAuthService
    {
        private readonly FirebaseAuth _firebaseAuth;

        public AuthFirebaseService(FirebaseAuth firebaseAuth)
        {
            this._firebaseAuth = firebaseAuth;
        }

        public async Task<Auth> CreateAsync(CreateAuth newAuth)
        {
            await this.AssertEmailInUse(newAuth.Email).ConfigureAwait(false);

            var userRecords = new UserRecordArgs
            {
                Uid = Db.NewId(),
                Email = newAuth.Email,
                Password = newAuth.Password,
                DisplayName = newAuth.FullName(),
            };

            await this.CreateAuthAsync(userRecords).ConfigureAwait(false);

            return new Auth(
                userRecords.Uid,
                newAuth.Email,
                userRecords.DisplayName);
        }

        private async Task AssertEmailInUse(string email)
        {
            try
            {
                var userWithEmail = await this._firebaseAuth.GetUserByEmailAsync(email).ConfigureAwait(false);
                var emailInUse = userWithEmail != null;

                if (emailInUse)
                {
                    throw new DuplicatedEmailException(email);
                }
            }
            catch (FirebaseAuthException ex)
            {
                if (ex.AuthErrorCode != AuthErrorCode.UserNotFound) throw;
            }
        }

        private async Task CreateAuthAsync(UserRecordArgs userRecords)
        {
            try
            {
                var firebaseAuth = await this._firebaseAuth.CreateUserAsync(userRecords).ConfigureAwait(false);
            }
            catch (FirebaseAuthException ex)
            {
                if (ex.AuthErrorCode == AuthErrorCode.EmailAlreadyExists)
                {
                    throw new DuplicatedEmailException(userRecords.Email);
                }

                throw;
            }
        }

        public async Task UpdatePasswordAsync(string newPassword, Auth userLogged)
        {
            var userUpdated = new UserRecordArgs
            {
                Uid = userLogged.Id,
                Password = newPassword,
            };

            var userRecord = await this._firebaseAuth.UpdateUserAsync(userUpdated).ConfigureAwait(false);
        }
    }
}