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

        private readonly ISessionRepository _sessionRepository;

        public AuthRepository(FirebaseAuth firebaseAuth, ISessionRepository sessionRepository)
        {
            _firebaseAuth = firebaseAuth;
            _sessionRepository = sessionRepository;
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

        public async Task<CreateAuthResult> CreateAsync(Auth newAuth)
        {
            var userRecords = new UserRecordArgs
            {
                Uid = newAuth.Id,
                Email = newAuth.Email,
                Password = newAuth.Password,
                DisplayName = newAuth.Name,
            };

            var firebaseAuth = await CreateAuthAsync(userRecords).ConfigureAwait(false);

            var session = await _sessionRepository.CreateAsync(new CreateSessionCredentials(newAuth.Email, newAuth.Password)).ConfigureAwait(false);

            return new CreateAuthResult(
                userRecords.Uid,
                newAuth.Email,
                userRecords.DisplayName,
                session.Token);
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

        public async Task UpdateAsync(Auth userLogged)
        {
            var userUpdated = new UserRecordArgs
            {
                Uid = userLogged.Id,
                Password = userLogged.Password,
            };

            var userRecord = await _firebaseAuth.UpdateUserAsync(userUpdated).ConfigureAwait(false);
        }
    }
}