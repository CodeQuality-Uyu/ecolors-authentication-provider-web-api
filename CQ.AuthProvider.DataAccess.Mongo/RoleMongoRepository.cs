using CQ.AuthProvider.DataAccess.Mongo.Authorizations;
using CQ.Exceptions;
using CQ.UnitOfWork.MongoDriver;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.Mongo
{
    internal sealed class RoleMongoRepository : MongoDriverRepository<RoleMongo>
    {
        public RoleMongoRepository(MongoContext mongoContext) : base(mongoContext)
        {
        }

        public override async Task<RoleMongo> GetByPropAsync(string value, string prop)
        {
            var role = await base.GetOrDefaultByPropAsync(value, prop).ConfigureAwait(false);

            if (Guard.IsNull(role))
                throw new SpecificResourceNotFoundException<Role>(value, prop);

            return role;
        }
    }
}
