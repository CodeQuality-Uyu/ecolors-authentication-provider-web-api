using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.DataAccess.Accounts;
using CQ.AuthProvider.DataAccess.ClientSystems;
using CQ.AuthProvider.DataAccess.Contexts;
using CQ.AuthProvider.DataAccess.Roles;
using CQ.IdentityProvider.EfCore.AppConfig;
using CQ.IdentityProvider.Firebase.AppConfig;
using CQ.IdentityProvider.Mongo.AppConfig;
using CQ.ServiceExtension;
using CQ.UnitOfWork;
using CQ.UnitOfWork.EfCore;
using CQ.UnitOfWork.MongoDriver;
using CQ.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.AppConfig
{
    public static class DataAccessConfig
    {
        private static IServiceCollection AddIdentityProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var identity = configuration
                .GetSection(ConfigOptions.Identity)
                .Get<IdentityOptions>();
            
            Guard.ThrowIsNull(identity, "Identity");

            if (identity.Type == IdentityType.Database)
            {
                var databaseIdentity = configuration
                    .GetSection(ConfigOptions.Identity)
                    .Get<IdentityDatabaseOptions>();

                Guard.ThrowIsNull(databaseIdentity, "Identity as database");
                Guard.ThrowIsNullOrEmpty(databaseIdentity.DatabaseName, "Identity:DatabaseName");

                var connectionString = configuration.GetConnectionString(ConnectionStrings.Identity);
                Guard.ThrowIsNullOrEmpty(connectionString, "ConnectionStrings:Identity");

                if (databaseIdentity.Engine == DatabaseEngine.Sql)
                {
                    services.AddEfCoreRepositories(
                        databaseIdentity.DatabaseName,
                        connectionString);
                }

                if (databaseIdentity.Engine == DatabaseEngine.Mongo)
                {
                    services.AddMongoRepositories(
                        databaseIdentity.DatabaseName,
                        connectionString);
                }
            }

            if (identity.Type == IdentityType.Firebase)
            {
                var firebaseIdentity = configuration
                    .GetSection(ConfigOptions.Identity)
                    .Get<IdentityFirebaseOptions>();
                
                Guard.ThrowIsNull(firebaseIdentity, "Identity as firebase");

                services.AddFirebaseRepositories(configuration, firebaseIdentity);
            }

            return services;
        }
    }
}
