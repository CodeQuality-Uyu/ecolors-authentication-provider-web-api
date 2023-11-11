using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.Exceptions;
using CQ.Utility;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;
using PlayerFinder.Auth.Core.Exceptions;
using System.Net.Http.Json;
using System.Security.Authentication;

namespace CQ.AuthProvider.Firebase
{
    internal class AuthRepository : IIdentityProviderRepository, IIdentityProviderHealthService
    {
        private readonly FirebaseAuth _firebaseAuth;
        private readonly ISettingsService _settingsService;

        public AuthRepository(FirebaseAuth firebaseAuth, ISettingsService settingsService)
        {
            this._firebaseAuth = firebaseAuth;
            this._settingsService = settingsService;
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

        public async Task CreateAsync(Identity identity)
        {
            var userRecords = new UserRecordArgs
            {
                Uid = identity.Id,
                Email = identity.Email,
                Password = identity.Password,
            };

            var firebaseAuth = await CreateAuthAsync(userRecords).ConfigureAwait(false);
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
                    throw new ResourceDuplicatedException("email", userRecords.Email, "FirebaseAuth");
                }

                throw;
            }
        }

        public async Task UpdatePasswordAsync(string newPassword, string identityId)
        {
            var userUpdated = new UserRecordArgs
            {
                Uid = identityId,
                Password = newPassword,
            };

            var userRecord = await _firebaseAuth.UpdateUserAsync(userUpdated).ConfigureAwait(false);
        }

        public string GetName()
        {
            return $"Firebase-{this._settingsService.GetValue(EnvironmentVariable.Firebase.ProjectName)}";
        }

        public bool Ping()
        {
            try
            {
                var task = this._firebaseAuth.GetUserByEmailAsync("ping@gmail.com");

                task.Wait();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}