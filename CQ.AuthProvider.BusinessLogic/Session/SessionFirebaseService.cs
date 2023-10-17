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
            try
            {

            var apiKey = _settingsService.GetValue(EnvironmentVariable.FirebaseApiKey);

            var response = await this._firebaseApi.PostAsync<SessionFirebase, LoginFirebaseError>(
                $"v1/accounts:signInWithPassword?key={apiKey}",
                new { email = credentials.Email, password = credentials.Password, returnSecureToken = true },
                (error) =>
                {
                    ProcessLoginError(error, credentials);
                })
                .ConfigureAwait(false);

            return new Session
            {
                Id = response.LocalId,
                Email = response.Email,
                Token = response.RefreshToken,
            };
            }catch(Exception ex)
            {
                throw;
            }
        }

        private void ProcessLoginError(dynamic error, CreateSessionCredentials credentials)
        {
            if (error.Error.AuthCode == FirebaseAuthErrorCode.EmailNotFound ||
            error.Error.AuthCode == FirebaseAuthErrorCode.InvalidPassword) throw new InvalidCredentialsException(credentials.Email, error.Error.AuthCode);

            if (error.Error.AuthCode == FirebaseAuthErrorCode.UserDisabled) throw new AuthDisabledException(credentials.Email);
        }
    }
}
