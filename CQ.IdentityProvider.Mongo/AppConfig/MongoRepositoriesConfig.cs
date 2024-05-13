using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.ServiceExtension;
using CQ.UnitOfWork;
using CQ.UnitOfWork.MongoDriver;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.IdentityProvider.Mongo.AppConfig
{
    public static class MongoRepositoriesConfig
    {
        public static IServiceCollection AddMongoRepositories(
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
            LifeTime.Scoped,
            LifeTime.Scoped)
                .AddMongoRepository<Session>(name, LifeTime.Scoped)
                .AddAbstractionMongoRepository<Identity, IIdentityProviderRepository, IdentityRepository>(name, LifeTime.Scoped)
                .AddScoped<IIdentityProviderHealthService, IdentityRepository>();

            return services;
        }
    }
}
