using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.UnitOfWork.Abstractions;
using CQ.Utility;

namespace CQ.AuthProvider.BusinessLogic.Sessions
{
    internal sealed class SessionService : ISessionInternalService
    {
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<Identity> _identityRepository;
        private readonly IAccountInfoRepository _accountRepository;
        private readonly HttpClientAdapter _httpClient;
        private readonly AuthOptions _authOptions;

        public SessionService(
            IRepository<Session> sessionRepository,
            IRepository<Identity> identityRepository,
            IAccountInfoRepository accountRepository,
            HttpClientAdapter httpClient,
            AuthOptions authOptions)
        {
            _sessionRepository = sessionRepository;
            _identityRepository = identityRepository;
            _accountRepository = accountRepository;
            _httpClient = httpClient;
            _authOptions = authOptions;
        }

        public async Task<SessionCreated> CreateAsync(CreateSessionCredentialsArgs credentials)
        {
            var identity = await _identityRepository
                .GetOrDefaultAsync(a =>
                a.Email == credentials.Email && a.Password == credentials.Password)
                .ConfigureAwait(false);

            if (Guard.IsNull(identity))
            {
                throw new InvalidCredentialsException(credentials.Email);
            }

            var sessionOfUser = await _sessionRepository.GetOrDefaultAsync(s => s.AccountId == identity.Id).ConfigureAwait(false);

            if (Guard.IsNull(sessionOfUser))
            {
                sessionOfUser = await CreateAsync(identity).ConfigureAwait(false);
            }
            else
            {
                sessionOfUser.Token = Db.NewId();

                await _sessionRepository.UpdateAsync(sessionOfUser).ConfigureAwait(false);
            }

            var account = await this._accountRepository.GetInfoByIdAsync(sessionOfUser.AccountId).ConfigureAwait(true);

            var sessionSaved = new SessionCreated(
                account,
                sessionOfUser.Email,
                sessionOfUser.Token);

            if (Guard.IsNotNullOrEmpty(credentials.ListenerServer))
            {
                try
                {
                    _httpClient.PostAsync($"{credentials.ListenerServer}", sessionSaved, new List<Header> { new ("PrivateKey", _authOptions.PrivateKey) });
                }
                catch (Exception) { }
            }

            return sessionSaved;
        }

        public async Task<Session> CreateAsync(Identity identity)
        {
            var session = new Session(identity.Id, identity.Email);

            var sessionCreated = await _sessionRepository.CreateAsync(session).ConfigureAwait(false);

            return sessionCreated;
        }

        public async Task<Session> GetByTokenAsync(string token)
        {
            var isGuid = Guid.TryParse(token, out var id);

            if (!isGuid)
                throw new ArgumentException("The token must be a valid Guid", "token");

            var result = await GetOrDefaultByTokenAsync(token).ConfigureAwait(false);

            if (result == null)
                throw new ArgumentException("Invalid token, session has expired", "token");

            return result;
        }

        public async Task<Session?> GetOrDefaultByTokenAsync(string token)
        {
            var isGuid = Guid.TryParse(token, out var id);

            if (!isGuid)
                return null;

            return await this._sessionRepository.GetOrDefaultByPropAsync(token, nameof(Session.Token)).ConfigureAwait(false);
        }

        public Task<bool> IsTokenValidAsync(string token)
        {
            var isGuid = Guid.TryParse(token, out var id);

            return Task.FromResult(isGuid);
        }
    }
}
