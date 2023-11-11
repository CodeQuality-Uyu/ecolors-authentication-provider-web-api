using CQ.AuthProvider.BusinessLogic;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.ServiceExtension;
using CQ.UnitOfWork;
using CQ.UnitOfWork.MongoDriver;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.Mongo.AppConfig
{
    public static class MongoDependencyInjection
    {
        public static IServiceCollection AddMongoServices(this IServiceCollection services, ISettingsService settingsService, string connectionString)
        {
            var databaseName = settingsService.GetValue(EnvironmentVariable.MongoIdentityProvider.DataBaseName);
            services
                .AddMongoContext<IdentityProviderMongoContext>(new MongoConfig
                {
                    DatabaseConnection = new DatabaseConfig
                    {
                        ConnectionString = connectionString,
                        DatabaseName = databaseName
                    },
                    UseDefaultQueryLogger = true
                },
            LifeTime.Singleton,
            LifeTime.Singleton)
                .AddMongoRepository<Session>(databaseName, LifeTime.Singleton)
                .AddCustomMongoRepository<Identity, IIdentityProviderRepository, IdentityRepository>(databaseName, LifeTime.Singleton)
                .AddSingleton<IIdentityProviderHealthService, IdentityRepository>()
                .AddSingleton<ISessionService, SessionService>();


            return services;
        }
    }
}
