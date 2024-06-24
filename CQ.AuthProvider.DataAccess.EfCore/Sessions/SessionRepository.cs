
using AutoMapper;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
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
    public async Task CreateAsync(Session session)
    {
        var sessionEfCore = new SessionEfCore(
            session.Id,
            session.Token,
            session.Account.Id);

        await CreateAsync(sessionEfCore).ConfigureAwait(false);
    }

    public async Task<Session?> GetOrDefaultByTokenAsync(string token)
    {
        var query = _dbSet
            .Include(s => s.Account)
            .Where(s => s.Token == token);

        var session = await query
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        if (Guard.IsNull(session))
        {
            return null;
        }

        return mapper.Map<Session>(session);
    }

    public async Task DeleteByTokenAsync(string token)
    {
        await DeleteAsync(s => s.Token == token).ConfigureAwait(false);
    }
}
