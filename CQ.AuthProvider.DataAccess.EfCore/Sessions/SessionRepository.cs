using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.Exceptions;
using CQ.UnitOfWork.EfCore.Core;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore.Sessions;

internal sealed class SessionRepository(
    AuthDbContext context,
    IMapper mapper)
    : EfCoreRepository<SessionEfCore>(context),
    ISessionRepository
{
    async Task ISessionRepository.CreateAsync(Session session)
    {
        var sessionEfCore = new SessionEfCore(session);

        await CreateAsync(sessionEfCore).ConfigureAwait(false);
    }

    public async Task<Session> GetByTokenAsync(string token)
    {
        var query = _entities
            .Include(s => s.Account)
            .Where(s => s.Token == token);

        var session = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        if (Guard.IsNull(session))
        {
            throw new SpecificResourceNotFoundException<Session>(nameof(Session.Token), token);
        }

        return mapper.Map<Session>(session);
    }

    public async Task DeleteByTokenAsync(string token)
    {
        await DeleteAndSaveAsync(s => s.Token == token)
            .ConfigureAwait(false);
    }
}
