using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.Utility;
using FirebaseAdmin.Auth;

namespace CQ.AuthProvider.Firebase
{
    internal class SessionService : ISessionInternalService
    {
        private readonly HttpClientAdapter _firebaseApi;
        private readonly FirebaseAuth _firebaseAuth;
        private readonly IdentityFirebaseOptions _options;
        private readonly IAccountInfoRepository _accountRepository;
        private readonly HttpClientAdapter _httpClient;
        private readonly AuthOptions _authOptions;

        public SessionService(
            IdentityFirebaseOptions options,
            FirebaseAuth firebaseAuth,
            HttpClientAdapter firebaseApi,
            IAccountInfoRepository accountRepository,
            HttpClientAdapter httpClient,
            AuthOptions authOptions)
        {
            this._options = options;
            this._firebaseApi = firebaseApi;
            this._firebaseAuth = firebaseAuth;
            this._accountRepository = accountRepository;
            _httpClient = httpClient;
            _authOptions = authOptions;
        }

        public async Task<SessionCreated> CreateAsync(CreateSessionCredentialsArgs credentials)
        {
            var response = await this._firebaseApi.PostAsync<SessionFirebase, FirebaseError>(
                $"v1/accounts:signInWithPassword?key={this._options.ApiKey}",
                new { email = credentials.Email, password = credentials.Password, returnSecureToken = true },
                (error) =>
                {
                    return this.ProcessLoginError(error, credentials.Email);
                })
                .ConfigureAwait(false);

            var account = await this._accountRepository.GetInfoByIdAsync(response.LocalId).ConfigureAwait(false);

            var sessionSaved = new SessionCreated(
                response.LocalId,
                response.Email,
                response.IdToken,
                account.FirstName,
                account.LastName,
                account.FullName,
                account.Roles,
                account.Permissions);

            if (Guard.IsNotNullOrEmpty(credentials.ListenerServer))
            {
                try
                {
                    _httpClient.PostAsync($"{credentials.ListenerServer}", sessionSaved, new List<Header> { new("PrivateKey", _authOptions.PrivateKey) });
                }
                catch (Exception) { }
            }

            return sessionSaved;
        }

        public async Task<Session> CreateAsync(Identity identity)
        {
            var response = await this._firebaseApi.PostAsync<SessionFirebase, FirebaseError>(
               $"v1/accounts:signInWithPassword?key={this._options.ApiKey}",
               new { email = identity.Email, password = identity.Password, returnSecureToken = true },
               (error) =>
               {
                   return this.ProcessLoginError(error, identity.Email);
               })
               .ConfigureAwait(false);

            return new Session(response.LocalId, identity.Email, response.IdToken);
        }

        private Exception? ProcessLoginError(FirebaseError error, string email)
        {
            if (error.Error.AuthCode == FirebaseAuthErrorCode.EmailNotFound ||
            error.Error.AuthCode == FirebaseAuthErrorCode.InvalidPassword) return new InvalidCredentialsException(email);

            if (error.Error.AuthCode == FirebaseAuthErrorCode.UserDisabled) return new AuthDisabledException(email);

            return null;
        }

        public async Task<Session> GetByTokenAsync(string token)
        {
            var result = await this._firebaseAuth.VerifyIdTokenAsync(token).ConfigureAwait(false);

            if (result == null) throw new ArgumentException("The token must be a valid JWT", "token");

            var auth = await this._accountRepository.GetInfoByIdAsync(result.Uid).ConfigureAwait(false);

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
