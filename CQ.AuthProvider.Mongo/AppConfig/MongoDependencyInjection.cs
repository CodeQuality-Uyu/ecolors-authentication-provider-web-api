using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.ServiceExtension;
using CQ.UnitOfWork;
using CQ.UnitOfWork.MongoDriver;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.Mongo.AppConfig
{
    public static class MongoDependencyInjection
    {
        public static IServiceCollection AddMongoServices(
            this IServiceCollection services,
            string name,
            string connectionString)
        {
            services
                .AddMongoContext<IdentityProviderMongoContext>(
                new MongoConfig(
                    new DatabaseConfig(connectionString, name),
                    useDefaultQueryLogger: true,
                    @default: false),
            LifeTime.Singleton,
            LifeTime.Singleton)
                .AddMongoRepository<Session>(name, LifeTime.Singleton)
                .AddAbstractionMongoRepository<Identity, IIdentityProviderRepository, IdentityRepository>(name, LifeTime.Singleton)
                .AddSingleton<IIdentityProviderHealthService, IdentityRepository>();

            return services;
        }
    }
}
