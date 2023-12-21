using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.Firebase.AppConfig;
using CQ.AuthProvider.Mongo.AppConfig;
using CQ.ServiceExtension;
using CQ.UnitOfWork;
using CQ.UnitOfWork.MongoDriver;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.AppConfig
{
    public static class DataAccessDependencyInjection
    {
        public static IServiceCollection AddCQDataAccess(this IServiceCollection services)
        {
            var settingsService = new SettingsService();

            services
                .AddMongoContext<AuthMongoContext>(new MongoConfig
                {
                    DatabaseConnection = new DatabaseConfig
                    {
                        ConnectionString = settingsService.GetValue(EnvironmentVariable.MongoAuth.ConnectionString),
                        DatabaseName = settingsService.GetValue(EnvironmentVariable.MongoAuth.DataBaseName)
                    },
                    UseDefaultQueryLogger = true,
                },
                LifeTime.Singleton,
                LifeTime.Singleton)
                .AddMongoRepository<Auth>(lifeTime: LifeTime.Singleton)
                .AddCustomMongoRepository<Role, IRoleRepository, RoleRepository>(lifeTime: LifeTime.Singleton)
                .AddMongoRepository<Permission>(lifeTime: LifeTime.Singleton)
                .AddMongoRepository<ResetPasswordApplication>(lifeTime: LifeTime.Singleton)
                .AddIdentityProvider(settingsService);

            return services;
        }

        private static IServiceCollection AddIdentityProvider(this IServiceCollection services, ISettingsService settingsService)
        {
            var mongoConnection = settingsService.GetValueOrDefault(EnvironmentVariable.MongoIdentityProvider.ConnectionString);

            if (!string.IsNullOrEmpty(mongoConnection))
            {
                services.AddMongoServices(settingsService, mongoConnection);

                return services;
            }


            settingsService.GetValue(EnvironmentVariable.Firebase.ProjectId);

            services.AddFirebaseServices(settingsService);

            return services;
        }
    }
}
