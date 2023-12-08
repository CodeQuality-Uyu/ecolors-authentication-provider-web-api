using Amazon.SecurityToken.Model;
using CQ.AuthProvider.BusinessLogic;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.MongoDriver;
using CQ.UnitOfWork.MongoDriver.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp;

namespace CQ.AuthProvider.Mongo
{
    public sealed class SessionService : ISessionService
    {
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<Identity> _identityRepository;
        private readonly IRepository<Auth> _authRepository;

        public SessionService(
            IRepository<Session> sessionRepository,
            IRepository<Identity> identityRepository,
            IRepository<Auth> authRepository)
        {
            _sessionRepository = sessionRepository;
            _identityRepository = identityRepository;
            _authRepository = authRepository;
        }

        public async Task<SessionCreated> CreateAsync(CreateSessionCredentials credentials)
        {
            var identity = await _identityRepository
                .GetOrDefaultAsync(a =>
            a.Email == credentials.Email && a.Password == credentials.Password)
                .ConfigureAwait(false);

            if (identity == null)
            {
                throw new InvalidCredentialsException(credentials.Email);
            }

            var sessionOfUser = await _sessionRepository.GetOrDefaultAsync(s => s.AuthId == identity.Id).ConfigureAwait(false);

            if (sessionOfUser == null)
            {
                sessionOfUser = await CreateNewAsync(identity).ConfigureAwait(false);
            }
            else
            {
                sessionOfUser = sessionOfUser with { Token = Guid.NewGuid().ToString() };

                await _sessionRepository.UpdateByIdAsync(sessionOfUser.Id, new { sessionOfUser.Token }).ConfigureAwait(false);
            }

            var auth = await this._authRepository.GetByPropAsync(identity.Id).ConfigureAwait(true);

            return new SessionCreated(
                sessionOfUser.AuthId,
                sessionOfUser.Email,
                sessionOfUser.Token,
                auth.Roles);
        }

        private async Task<Session> CreateNewAsync(Identity identity)
        {
            var session = new Session(identity.Id, identity.Email, Guid.NewGuid().ToString());

            var sessionCreated = await _sessionRepository.CreateAsync(session).ConfigureAwait(false);

            return sessionCreated;
        }

        public async Task<Session> GetByTokenAsync(string token)
        {
            var isGuid = Guid.TryParse(token, out var id);

            if (!isGuid) throw new ArgumentException("The token must be a valid Guid", "token");

            var result = await GetOrDefaultByTokenAsync(token).ConfigureAwait(false);

            if (result == null) throw new ArgumentException("Invalid token, session has expired", "token");

            return result;
        }

        public async Task<Session?> GetOrDefaultByTokenAsync(string token)
        {
            var isGuid = Guid.TryParse(token, out var id);

            if (!isGuid) return null;

            return await this._sessionRepository.GetOrDefaultByPropAsync(token, nameof(Session.Token)).ConfigureAwait(false);
        }

        public async Task<bool> IsTokenValidAsync(string token)
        {
            var isGuid = Guid.TryParse(token, out var id);

            if (!isGuid) return false;

            return await this._sessionRepository.ExistAsync(s => s.Token == token).ConfigureAwait(false);
        }
    }
}
