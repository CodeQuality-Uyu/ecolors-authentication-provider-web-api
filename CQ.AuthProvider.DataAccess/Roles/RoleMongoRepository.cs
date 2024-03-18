using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.Exceptions;
using CQ.UnitOfWork.MongoDriver;
using CQ.Utility;

namespace CQ.AuthProvider.DataAccess.Roles
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
                throw new SpecificResourceNotFoundException<RoleInfo>(value, prop);

            return role;
        }
    }
}
