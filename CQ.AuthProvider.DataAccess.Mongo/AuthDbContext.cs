using CQ.AuthProvider.DataAccess.Mongo.Accounts;
using CQ.AuthProvider.DataAccess.Mongo.Permissions;
using CQ.AuthProvider.DataAccess.Mongo.ResetPasswords;
using CQ.AuthProvider.DataAccess.Mongo.Roles;
using CQ.AuthProvider.DataAccess.Mongo.Sessions;
using CQ.UnitOfWork.MongoDriver.Core;
using MongoDB.Driver;

namespace CQ.AuthProvider.DataAccess.Mongo;

public sealed class AuthDbContext : MongoContext
{
    public AuthDbContext(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
        base
            .AddCollection<AccountMongo>("Accounts")
            .AddCollection<RoleMongo>("Roles")
            .AddCollection<PermissionMongo>("Permissions")
            .AddCollection<SessionMongo>("Sessions")
            .AddCollection<ResetPasswordMongo>("ResetPassword");
    }
}
