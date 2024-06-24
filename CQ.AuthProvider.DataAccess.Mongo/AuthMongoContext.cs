using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.DataAccess.Mongo.Accounts;
using CQ.AuthProvider.DataAccess.Mongo.Authorizations;
using CQ.UnitOfWork.MongoDriver;
using MongoDB.Driver;

namespace CQ.AuthProvider.DataAccess.Mongo
{
    internal sealed class AuthMongoContext : MongoContext
    {
        public AuthMongoContext(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
            base
                .AddCollection<AccountMongo>("Accounts")
                .AddCollection<RoleMongo>("Roles")
                .AddCollection<PermissionMongo>("Permissions")
                .AddCollection<ResetPasswordApplication>("ResetPasswordApplications");
        }
    }
}
