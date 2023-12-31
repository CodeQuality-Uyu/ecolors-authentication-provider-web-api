using CQ.AuthProvider.BusinessLogic;
using CQ.UnitOfWork.MongoDriver;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.Mongo
{
    internal sealed class IdentityProviderMongoContext : MongoContext
    {
        public IdentityProviderMongoContext(IMongoDatabase mongoDatabase, bool isDefault) : base(mongoDatabase, isDefault)
        {
            base.AddCollection<Identity>("Auths");
        }
    }
}
