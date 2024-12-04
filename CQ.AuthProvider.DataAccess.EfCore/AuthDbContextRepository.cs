using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore;
internal class AuthDbContextRepository<TEntity>(AuthDbContext _context)
    : EfCoreRepositoryContext<TEntity, AuthDbContext>(_context)
    where TEntity : class
{
    protected IQueryable<TEntity> InsertIncludes(
        IQueryable<TEntity> query,
        List<string> includes)
    {
        IQueryable<TEntity> queryInclude = query;

        includes.ForEach(include =>
        {
            queryInclude = queryInclude
            .Include(include);
        });

        return queryInclude;
    }
}
