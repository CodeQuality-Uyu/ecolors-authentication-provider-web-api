using CQ.AuthProvider.BusinessLogic;
using CQ.UnitOfWork.MongoDriver;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess
{
    internal sealed class RoleMongoRepository : MongoDriverRepository<Role>, IRoleRepository
    {
        public RoleMongoRepository(MongoContext mongoContext) : base(mongoContext)
        {
        }

        public async Task AddPermissionsByIdAsync(string id, List<Permission> permissions)
        {
            var filter = Builders<Role>.Filter.Eq(r => r.Id, id);
            var update = Builders<Role>.Update.PushEach(r => r.Permissions, permissions);

            await base._collection.UpdateOneAsync(filter, update).ConfigureAwait(false); 
        }
    }
}
