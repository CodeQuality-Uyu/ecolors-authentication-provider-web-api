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
    internal sealed class AuthMongoContext : MongoContext
    {
        public AuthMongoContext(IMongoDatabase mongoDatabase) : base(mongoDatabase, true)
        {
            base
                .AddCollection<Auth>("Auths")
                .AddCollection<Role>("Roles")
                .AddCollection<Permission>("Permissions");
        }
    }
}
