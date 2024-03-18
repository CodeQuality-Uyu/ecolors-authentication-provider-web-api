using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.UnitOfWork.MongoDriver;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.Contexts
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
