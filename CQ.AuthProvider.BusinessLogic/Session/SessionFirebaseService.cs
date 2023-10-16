using CQ.Utility;
using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic
{
    public class SessionFirebaseService : ISessionService
    {
        private readonly HttpClientAdapter _firebaseApi;

        public SessionFirebaseService(HttpClientAdapter firebaseApi)
        {
            this._firebaseApi = firebaseApi;
        }

        public async Task<Session> CreateAsync(CreateSessionCredentials credentials)
        {
            var apiKey = Environment.GetEnvironmentVariable("firebase:api-key");

            var response = await this._firebaseApi.PostAsync<SessionFirebase, object>($"accounts:signInWithPassword?key={apiKey}", new { email = credentials.Email, password = credentials.Password, returnSecureToken = true }).ConfigureAwait(false);

            return new Session
            {
                Id = response.LocalId,
                Email = response.Email,
                Token = response.RefreshToken,
            };
        }
    }
}
