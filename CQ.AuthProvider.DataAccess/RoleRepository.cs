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
    internal sealed class RoleRepository : MongoDriverRepository<Role>,IRoleRepository
    {
        public RoleRepository(MongoContext mongoContext) : base(mongoContext)
        {
        }

        public async Task AddPermissionsByIdAsync(string id, IList<string> permissionsKeys)
        {
            var filter = Builders<Role>.Filter.Eq(r => r.Id, id);
            var update = Builders<Role>.Update.PushEach(r => r.PermissionKeys, permissionsKeys);

            await base._collection.UpdateOneAsync(filter, update).ConfigureAwait(false); 
        }
    }
}
