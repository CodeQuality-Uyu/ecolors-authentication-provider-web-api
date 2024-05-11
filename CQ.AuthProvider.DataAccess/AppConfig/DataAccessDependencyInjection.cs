using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.DataAccess.Accounts;
using CQ.AuthProvider.DataAccess.ClientSystems;
using CQ.AuthProvider.DataAccess.Contexts;
using CQ.AuthProvider.DataAccess.Roles;
using CQ.AuthProvider.EfCore.AppConfig;
using CQ.AuthProvider.Firebase.AppConfig;
using CQ.AuthProvider.Mongo.AppConfig;
using CQ.ServiceExtension;
using CQ.UnitOfWork;
using CQ.UnitOfWork.EfCore;
using CQ.UnitOfWork.MongoDriver;
using CQ.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.AppConfig
{
    public static class DataAccessDependencyInjection
    {
        public static IServiceCollection AddCQDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConnectionStrings.Auth);

            var authOption = configuration
                .GetSection(ConfigOptions.Auth)
                .Get<AuthOptions>();

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

            services.AddIdentityProvider(configuration);

            return services;
        }

        private static IServiceCollection AddIdentityProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var identity = configuration
                .GetSection(ConfigOptions.Identity)
                .Get<IdentityOptions>();

            Guard.ThrowIsNull(identity.Type, "Identity:Type");

            if (identity.Type == IdentityType.Database)
            {
                var databaseIdentity = configuration
                    .GetSection(ConfigOptions.Identity)
                    .Get<IdentityDatabaseOptions>()!;

                var connectionString = configuration.GetConnectionString(ConnectionStrings.Identity);

                Guard.ThrowIsNull(databaseIdentity.Engine, "Identity:Engine");
                Guard.ThrowIsNullOrEmpty(databaseIdentity.DatabaseName, "Identity:DatabaseName");
                Guard.ThrowIsNullOrEmpty(connectionString, "ConnectionStrings:Identity");

                if (databaseIdentity.Engine == DatabaseEngine.Mongo)
                {
                    services.AddMongoServices(
                        databaseIdentity.DatabaseName,
                        connectionString);
                }

                if (databaseIdentity.Engine == DatabaseEngine.Sql)
                {
                    services.AddEfCoreServices(
                        databaseIdentity.DatabaseName,
                        connectionString);
                }
            }


            if (identity.Type == IdentityType.Firebase)
            {
                var firebaseIdentity = configuration
                    .GetSection(ConfigOptions.Identity)
                    .Get<IdentityFirebaseOptions>()!;

                services.AddFirebaseServices(configuration, firebaseIdentity);
            }

            return services;
        }
    }
}
