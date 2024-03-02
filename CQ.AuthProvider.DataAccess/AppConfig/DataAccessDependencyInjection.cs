using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.DataAccess.Accounts;
using CQ.AuthProvider.DataAccess.Contexts;
using CQ.AuthProvider.DataAccess.Permissions;
using CQ.AuthProvider.DataAccess.ResetPasswordApplications;
using CQ.AuthProvider.DataAccess.Roles;
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
                    .AddMongoContext<AuthMongoContext>(
                    new MongoConfig(
                        new DatabaseConfig(mongoConnectionString, databaseName),
                        useDefaultQueryLogger: true),
                    LifeTime.Singleton,
                    LifeTime.Singleton)
                    .AddAbstractionMongoRepository<AccountMongo, IAccountRepository<AccountMongo>, AccountMongoRepository>(LifeTime.Singleton)
                    .AddAbstractionMongoRepository<AccountMongo, IAccountInfoRepository, AccountMongoRepository>(LifeTime.Singleton)
                    .AddAbstractionMongoRepository<RoleMongo, IRoleRepository<RoleMongo>, RoleMongoRepository>(LifeTime.Singleton)
                    .AddAbstractionMongoRepository<ResetPasswordApplication, IResetPasswordApplicationRepository<ResetPasswordApplication>, ResetPasswordApplicationMongoRepository>(LifeTime.Singleton)
                    .AddAbstractionMongoRepository<Permission, IPermissionRepository<Permission>, PermissionMongoRepository>(LifeTime.Singleton);
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
                    .AddAbstractionEfCoreRepository<AccountEfCore, IAccountRepository<AccountEfCore>, AccountEfCoreRepository>(LifeTime.Singleton)
                    .AddAbstractionEfCoreRepository<AccountEfCore, IAccountInfoRepository, AccountEfCoreRepository>(LifeTime.Singleton)
                    .AddAbstractionEfCoreRepository<RoleEfCore, IRoleRepository<RoleEfCore>, RoleEfCoreRepository>(LifeTime.Singleton)
                    .AddAbstractionEfCoreRepository<ResetPasswordApplicationEfCore, IResetPasswordApplicationRepository<ResetPasswordApplicationEfCore>, ResetPasswordApplicationEfCoreRepository>(LifeTime.Singleton)
                    .AddAbstractionEfCoreRepository<PermissionEfCore, IPermissionRepository<PermissionEfCore>, PermissionEfCoreRepository>(LifeTime.Singleton);
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
