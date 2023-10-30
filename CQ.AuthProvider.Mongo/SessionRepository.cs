using Amazon.SecurityToken.Model;
using CQ.AuthProvider.BusinessLogic;
using CQ.UnitOfWork.Abstractions;
using CQ.UnitOfWork.MongoDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.Mongo
{
    public class SessionRepository : MongoDriverRepository<Session>, ISessionRepository
    {
        private readonly IRepository<Auth> _authRepository;

        public SessionRepository(MongoContext context, IRepository<Auth> authRepository)
            : base(context, "Sessions")
        {
            _authRepository = authRepository;
        }

        public async Task<Session> CreateAsync(CreateSessionCredentials credentials)
        {
            var auth = await _authRepository
                .GetOrDefaultAsync(a =>
            a.Email == credentials.Email && a.Password == credentials.Password)
                .ConfigureAwait(false);

            if (auth == null)
            {
                throw new InvalidCredentialsException(credentials.Email);
            }

            var sessionOfUser = await base.GetOrDefaultAsync(s => s.AuthId == auth.Id).ConfigureAwait(false);

            if (sessionOfUser == null)
            {
                sessionOfUser = await CreateNewAsync(auth).ConfigureAwait(false);
            }
            else
            {
                sessionOfUser = sessionOfUser with { Token = Guid.NewGuid().ToString() };

                await base.UpdateByPropAsync(sessionOfUser.Id, new { Token = sessionOfUser.Token }).ConfigureAwait(false);
            }

            return sessionOfUser;
        }

        private async Task<Session> CreateNewAsync(Auth auth)
        {
            var session = new Session(auth.Id, auth.Email, Guid.NewGuid().ToString());

            var sessionCreated = await base.CreateAsync(session).ConfigureAwait(false);

            return sessionCreated;
        }
    }
}
