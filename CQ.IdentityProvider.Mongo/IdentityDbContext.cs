using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.UnitOfWork.MongoDriver.Core;
using MongoDB.Driver;

namespace CQ.IdentityProvider.Mongo
{
    internal sealed class IdentityDbContext 
        : MongoContext,
        IIdentityProviderHealthService
    {
        public IdentityDbContext(IMongoDatabase mongoDatabase) 
            : base(mongoDatabase)
        {
            base
                .AddCollection<Identity>("Identities");
        }

        public string GetProvider()
        {
            return "Mongo";
        }

        public bool Ping()
        {
            return base.Ping();
        }
    }
}
