using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.Utility;
using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    internal class SessionFirebaseService : ISessionService
    {
        private readonly ISettingsService _settingsService;
        private readonly HttpClientAdapter _firebaseApi;

        public SessionFirebaseService(
            ISettingsService settingsService,
            HttpClientAdapter firebaseApi)
        {
            _settingsService = settingsService;
            _firebaseApi = firebaseApi;
        }

        public async Task<Session> CreateAsync(CreateSessionCredentials credentials)
        {
            var apiKey = _settingsService.GetValue(EnvironmentVariable.Firebase.ApiKey);

            var response = await this._firebaseApi.PostAsync<SessionFirebase, FirebaseError>(
                $"v1/accounts:signInWithPassword?key={apiKey}",
                new { email = credentials.Email, password = credentials.Password, returnSecureToken = true },
                (error) =>
                {
                    ProcessLoginError(error, credentials);
                })
                .ConfigureAwait(false);

            return new Session(
                response.LocalId,
                response.Email,
                response.IdToken);
        }

        private void ProcessLoginError(dynamic error, CreateSessionCredentials credentials)
        {
            if (error.Error.AuthCode == FirebaseAuthErrorCode.EmailNotFound ||
            error.Error.AuthCode == FirebaseAuthErrorCode.InvalidPassword) throw new InvalidCredentialsException(credentials.Email, error.Error.AuthCode);

            if (error.Error.AuthCode == FirebaseAuthErrorCode.UserDisabled) throw new AuthDisabledException(credentials.Email);
        }
    }
}
