
using CQ.Exceptions;
using CQ.UnitOfWork.EfCore.Core;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.EfCore;
internal static class EfCoreRepositoryExtensions
{
    public static void AssertNullEntity<TEntity>(
        this EfCoreRepository<TEntity> repository,
        TEntity? entity,
        string propertyValue,
        string propertyName)
        where TEntity : class
    {
        if (Guard.IsNotNull(entity))
        {
            return;
        }

        throw new SpecificResourceNotFoundException<TEntity>(propertyName, propertyValue);
    }
}
