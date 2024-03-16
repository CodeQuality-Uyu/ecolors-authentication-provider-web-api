using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.UnitOfWork.MongoDriver;
using MongoDB.Driver;

namespace CQ.AuthProvider.Mongo
{
    internal sealed class IdentityProviderMongoContext : MongoContext
    {
        public IdentityProviderMongoContext(IMongoDatabase mongoDatabase) 
            : base(mongoDatabase)
        {
            base
                .AddCollection<Identity>("Identities")
                .AddCollection<Session>("Sessions");
        }
    }
}
