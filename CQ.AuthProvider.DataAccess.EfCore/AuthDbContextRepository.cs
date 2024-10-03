using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CQ.AuthProvider.DataAccess.EfCore;
internal class AuthDbContextRepository<TEntity>(AuthDbContext context)
    : EfCoreRepositoryContext<TEntity, AuthDbContext>(context)
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
