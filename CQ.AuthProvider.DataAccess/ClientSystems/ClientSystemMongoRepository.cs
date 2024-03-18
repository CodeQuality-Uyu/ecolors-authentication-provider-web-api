using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.Exceptions;
using CQ.UnitOfWork.MongoDriver;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.ClientSystems
{
    internal sealed class ClientSystemMongoRepository : MongoDriverRepository<ClientSystemMongo>
    {
        public ClientSystemMongoRepository(MongoContext mongoContext) : base(mongoContext)
        {
        }

        public override async Task<ClientSystemMongo> GetByPropAsync(string value, string prop)
        {
            var clientSystem = await base.GetOrDefaultByPropAsync(value, prop).ConfigureAwait(false);

            if (Guard.IsNull(clientSystem))
                throw new SpecificResourceNotFoundException<ClientSystem>(value, prop);

            return clientSystem;
        }
    }
}
