using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.UnitOfWork.MongoDriver.Core;
using MongoDB.Driver;

namespace CQ.IdentityProvider.Mongo
{
    internal sealed class IdentityProviderMongoContext 
        : MongoContext,
        IIdentityProviderHealthService
    {
        public IdentityProviderMongoContext(IMongoDatabase mongoDatabase) 
            : base(mongoDatabase)
        {
            base
                .AddCollection<Identity>("Identities");
        }

        public string GetProvider()
        {
            return "Mongo";
        }

        public string GetName()
        {
            return "";
        }

        public bool Ping()
        {
            return base.Ping();
        }
    }
}
