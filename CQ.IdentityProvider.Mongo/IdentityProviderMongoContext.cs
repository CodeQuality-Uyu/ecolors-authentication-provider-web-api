using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.UnitOfWork.MongoDriver;
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
            return base.GetDatabaseInfo().Provider;
        }

        public string GetName()
        {
            return base.GetDatabaseInfo().Name;
        }

        public bool Ping()
        {
            return base.Ping();
        }
    }
}
