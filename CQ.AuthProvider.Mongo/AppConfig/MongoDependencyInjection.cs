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
            services.AddMongoContext(new MongoConfig
            {
                DatabaseConnection = new DatabaseConfig
                {
                    ConnectionString = connectionString,
                    DatabaseName = settingsService.GetValue(EnvironmentVariable.Mongo.DataBaseName)
                },
            }, 
            LifeTime.Singleton, 
            LifeTime.Singleton);

            services.AddMongoRepository<Auth>("Auths",LifeTime.Singleton);
            services.AddMongoRepository<Session>("Sessions",LifeTime.Singleton);

            services.AddUnitOfWork(LifeTime.Singleton);

            services
                .AddSingleton<IAuthRepository, AuthRepository>()
                .AddSingleton<ISessionRepository, SessionRepository>();

            return services;
        }
    }
}
