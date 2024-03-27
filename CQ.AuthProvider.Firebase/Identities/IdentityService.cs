using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using FirebaseAdmin.Auth;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.Exceptions;

namespace CQ.AuthProvider.Firebase
{
    internal class IdentityService : IIdentityProviderRepository, IIdentityProviderHealthService
    {
        private readonly FirebaseAuth _firebaseAuth;

        private readonly IdentityFirebaseOptions _options;

        public IdentityService(IdentityFirebaseOptions options, FirebaseAuth firebaseAuth)
        {
            this._firebaseAuth = firebaseAuth;
            this._options = options;
        }

        public async Task<Identity> GetByEmailAsync(string email)
        {
            try
            {
                var userWithEmail = await _firebaseAuth.GetUserByEmailAsync(email).ConfigureAwait(false);

                if (userWithEmail == null)
                    throw new SpecificResourceNotFoundException<Identity>(nameof(Identity.Email), email);

                return new Identity
                {
                    Id = userWithEmail.Uid,
                    Email = email,
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
                    throw new SpecificResourceDuplicatedException<Account>(nameof(Account.Email), userRecords.Email);
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
            return $"Firebase-{this._options.ProjectName}";
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

        public async Task DeleteByIdAsync(string id)
        {
            await this._firebaseAuth.DeleteUserAsync(id).ConfigureAwait(false);
        }
    }
}