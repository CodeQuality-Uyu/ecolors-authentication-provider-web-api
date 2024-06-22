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
        public static IServiceCollection ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConnectionStrings.Auth);

            Guard.ThrowIsNullOrEmpty(connectionString, "ConnectionStrings:Auth");

            var authOption = configuration
                .GetSection(ConfigOptions.Auth)
                .Get<AuthOptions>();

            Guard.ThrowIsNull(authOption, "Auth");

            if (authOption.Engine == DatabaseEngine.Sql)
            {
                services
                    .AddEfCoreContext<AuthEfCoreContext>(
                    new EfCoreConfig(
                        new DatabaseConfig(connectionString, authOption.DatabaseName),
                        useDefaultQueryLogger: true),
                    LifeTime.Scoped,
                    LifeTime.Scoped)
                    .AddAbstractionEfCoreRepository<AccountEfCore, IAccountInfoRepository, AccountEfCoreRepository>(LifeTime.Scoped)
                    .AddEfCoreRepository<RolePermission>(LifeTime.Scoped)
                    .AddCustomEfCoreRepository<RoleEfCore, RoleEfCoreRepository>(LifeTime.Scoped)
                    .AddEfCoreRepository<ResetPasswordApplicationEfCore>(LifeTime.Scoped)
                    .AddEfCoreRepository<PermissionEfCore>(LifeTime.Scoped)
                    .AddCustomEfCoreRepository<ClientSystemEfCore, ClientSystemEfCoreRepository>(LifeTime.Scoped);
            }

            if (authOption.Engine == DatabaseEngine.Mongo)
            {
                services
                    .AddMongoContext<AuthMongoContext>(
                    new MongoConfig(
                        new DatabaseConfig(connectionString, authOption.DatabaseName),
                        useDefaultQueryLogger: true),
                    LifeTime.Scoped,
                    LifeTime.Scoped)
                    .AddAbstractionMongoRepository<AccountMongo, IAccountInfoRepository, AccountMongoRepository>(LifeTime.Scoped)
                    .AddCustomMongoRepository<RoleMongo, RoleMongoRepository>(LifeTime.Scoped)
                    .AddMongoRepository<ResetPasswordApplication>(LifeTime.Scoped)
                    .AddMongoRepository<PermissionMongo>(LifeTime.Scoped)
                    .AddCustomMongoRepository<ClientSystemMongo, ClientSystemMongoRepository>(LifeTime.Scoped);
            }

            services.AddIdentityProvider(configuration);

            return services;
        }

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
