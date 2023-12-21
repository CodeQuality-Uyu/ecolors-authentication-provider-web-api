using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;
using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.Firebase
{
    internal class SessionService : ISessionService
    {
        private readonly HttpClientAdapter _firebaseApi;
        private readonly FirebaseAuth _firebaseAuth;
        private readonly string _firebaseApiKey;
        private readonly IRepository<Auth> _authRepository;

        public SessionService(
            ISettingsService settingsService,
            FirebaseAuth firebaseAuth,
            HttpClientAdapter firebaseApi,
            IRepository<Auth> authRepository)
        {
            this._firebaseApiKey = settingsService.GetValue(EnvironmentVariable.Firebase.ApiKey);
            this._firebaseApi = firebaseApi;
            this._firebaseAuth = firebaseAuth;
            this._authRepository = authRepository;
        }

        public async Task<SessionCreated> CreateAsync(CreateSessionCredentials credentials)
        {

            var response = await this._firebaseApi.PostAsync<SessionFirebase, FirebaseError>(
                $"v1/accounts:signInWithPassword?key={this._firebaseApiKey}",
                new { email = credentials.Email, password = credentials.Password, returnSecureToken = true },
                (error) =>
                {
                    return this.ProcessLoginError(error, credentials);
                })
                .ConfigureAwait(false);
            var auth = await this._authRepository.GetByPropAsync(response.LocalId).ConfigureAwait(false);

            return new SessionCreated(
                response.LocalId,
                response.Email,
                response.IdToken,
                auth.Roles);
        }

        private Exception? ProcessLoginError(FirebaseError error, CreateSessionCredentials credentials)
        {
            if (error.Error.AuthCode == FirebaseAuthErrorCode.EmailNotFound ||
            error.Error.AuthCode == FirebaseAuthErrorCode.InvalidPassword) return new InvalidCredentialsException(credentials.Email);

            if (error.Error.AuthCode == FirebaseAuthErrorCode.UserDisabled) return new AuthDisabledException(credentials.Email);

            return null;
        }

        public async Task<Session> GetByTokenAsync(string token)
        {
            var result = await this._firebaseAuth.VerifyIdTokenAsync(token).ConfigureAwait(false);

            if (result == null) throw new ArgumentException("The token must be a valid JWT", "token");

            var auth = await this._authRepository.GetByPropAsync(result.Uid).ConfigureAwait(false);

            return new Session(result.Uid, auth.Email, token);
        }

        public async Task<Session?> GetOrDefaultByTokenAsync(string token)
        {
            var result = await this._firebaseAuth.VerifyIdTokenAsync(token).ConfigureAwait(false);

            if (result == null) return null;

            return new Session(result.Uid, "email", token);
        }

        public async Task<bool> IsTokenValidAsync(string token)
        {
            var result = await GetOrDefaultByTokenAsync(token).ConfigureAwait(false);

            return result != null;
        }
    }
}
