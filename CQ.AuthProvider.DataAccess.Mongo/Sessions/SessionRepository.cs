

using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.UnitOfWork.MongoDriver.Core;

namespace CQ.AuthProvider.DataAccess.Mongo.Sessions;

internal sealed class SessionRepository(
    AuthDbContext context,
    IMapper mapper)
    : MongoDriverRepository<SessionMongo>(context),
    ISessionRepository
{
    public Task CreateAsync(Session session)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByTokenAsync(string token)
    {
        throw new NotImplementedException();
    }

    public Task<Session?> GetByTokenAsync(string token)
    {
        throw new NotImplementedException();
    }
}
