using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.DataAccess.Context;
using CQ.AuthProvider.EfCore.AppConfig;
using CQ.AuthProvider.Firebase.AppConfig;
using CQ.AuthProvider.Mongo.AppConfig;
using CQ.ServiceExtension;
using CQ.UnitOfWork;
using CQ.UnitOfWork.EfCore;
using CQ.UnitOfWork.MongoDriver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.AppConfig
{
    public static class DataAccessDependencyInjection
    {
        public static IServiceCollection AddCQDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConnectionString = configuration.GetConnectionString("AuthMongo");
            var sqlConnectionString = configuration.GetConnectionString("AuthSql");
            var databaseName = configuration.GetValue<string>("DatabaseName");

            if (!string.IsNullOrEmpty(mongoConnectionString))
            {
                services
                    .AddMongoContext(
                    new MongoConfig(
                        new DatabaseConfig(mongoConnectionString, databaseName),
                        useDefaultQueryLogger: true),
                    LifeTime.Singleton,
                    LifeTime.Singleton)
                    .AddMongoRepository<Account>(LifeTime.Singleton)
                    .AddAbstractionMongoRepository<Role, IRoleRepository, RoleMongoRepository>(LifeTime.Singleton)
                    .AddMongoRepository<Permission>(LifeTime.Singleton)
                    .AddMongoRepository<ResetPasswordApplication>(LifeTime.Singleton);
            }

            if (!string.IsNullOrEmpty(sqlConnectionString))
            {
                services
                    .AddEfCoreContext<AuthEfCoreContext>(
                    new EfCoreConfig(
                        new DatabaseConfig(sqlConnectionString, databaseName),
                        useDefaultQueryLogger: true),
                    LifeTime.Singleton,
                    LifeTime.Singleton)
                    .AddCustomEfCoreRepository<Account, AccountEfCoreRepository>(LifeTime.Singleton)
                    .AddAbstractionEfCoreRepository<Role, IRoleRepository, RoleEfCoreRepository>(LifeTime.Singleton)
                    .AddEfCoreRepository<Permission>(LifeTime.Singleton)
                    .AddEfCoreRepository<ResetPasswordApplication>(LifeTime.Singleton);
            }

            services.AddIdentityProvider(configuration);

            return services;
        }

        private static IServiceCollection AddIdentityProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var identity = configuration
                .GetSection(IdentityOptions.Identity)
                .Get<IdentityOptions>()!;

            if (identity.Type == "Database")
            {
                var databaseIdentity = configuration
                    .GetSection(IdentityOptions.Identity)
                    .Get<IdentityDatabaseOptions>()!;

                if (databaseIdentity.Engine == "Mongo")
                {
                    services.AddMongoServices(
                        databaseIdentity.Name,
                        databaseIdentity.ConnectionString);

                }

                if (databaseIdentity.Engine == "Sql")
                {
                    services.AddEfCoreServices(
                        databaseIdentity.Name,
                        databaseIdentity.ConnectionString);
                }

                return services;
            }


            if (identity.Type == "Firebase")
            {
                var firebaseIdentity = configuration
                    .GetSection(IdentityOptions.Identity)
                    .Get<IdentityFirebaseOptions>()!;

                services.AddFirebaseServices(configuration, firebaseIdentity);

                return services;
            }

            throw new Exception("Identity provider type invalid");
        }
    }
}
