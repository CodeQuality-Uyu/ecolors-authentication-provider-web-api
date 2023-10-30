using CQ.AuthProvider.BusinessLogic;
using CQ.Utility;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;
using PlayerFinder.Auth.Core.Exceptions;
using System.Net.Http.Json;
using System.Security.Authentication;

namespace CQ.AuthProvider.Firebase
{
    internal class AuthRepository : IAuthRepository
    {
        private readonly FirebaseAuth _firebaseAuth;

        public AuthRepository(FirebaseAuth firebaseAuth)
        {
            _firebaseAuth = firebaseAuth;
        }

        public async Task<Auth> GetByEmailAsync(string email)
        {
            try
            {
                var userWithEmail = await _firebaseAuth.GetUserByEmailAsync(email).ConfigureAwait(false);

                if (userWithEmail == null)
                {
                    throw new AuthNotFoundException(email);
                }

                return new Auth
                {
                    Id = userWithEmail.Uid,
                    Email = email,
                    Name = userWithEmail.DisplayName,
                };
            }
            catch (FirebaseAuthException ex)
            {
                throw new AuthNotFoundException(email, ex);
            }
        }

        public async Task<bool> ExistByEmailAsync(string email)
        {
            try
            {
                await GetByEmailAsync(email).ConfigureAwait(false);
                return true;
            }
            catch (AuthNotFoundException) { return false; }
        }

        public async Task<Auth> CreateAsync(Auth newAuth)
        {
            var userRecords = new UserRecordArgs
            {
                Uid = newAuth.Id,
                Email = newAuth.Email,
                Password = newAuth.Password,
                DisplayName = newAuth.Name,
            };

            var firebaseAuth = await CreateAuthAsync(userRecords).ConfigureAwait(false);

            return newAuth;
        }

        private async Task<UserRecord> CreateAuthAsync(UserRecordArgs userRecords)
        {
            try
            {
                var firebaseAuth = await _firebaseAuth.CreateUserAsync(userRecords).ConfigureAwait(false);

                return firebaseAuth;
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

            var userRecord = await _firebaseAuth.UpdateUserAsync(userUpdated).ConfigureAwait(false);
        }
    }
}