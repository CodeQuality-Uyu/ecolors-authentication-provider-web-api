
using CQ.AuthProvider.BusinessLogic.Abstractions.Identities;
using CQ.Extensions.ServiceCollection;
using CQ.IdentityProvider.Mongo.Identities;
using CQ.UnitOfWork.MongoDriver.Configuration;
using CQ.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CQ.IdentityProvider.Mongo.AppConfig;

public static class MongoRepositoriesConfig
{
    public static IServiceCollection AddMongoRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Identity");
        Guard.ThrowIsNullOrEmpty(connectionString, "ConnectionStrings:Identity");

        services
            .AddContext<IdentityDbContext>(
            new MongoClient(connectionString),
            LifeTime.Scoped)
            .AddAbstractionRepository<Identity, IIdentityRepository, IdentityRepository>(LifeTime.Scoped)
            .AddScoped<IIdentityProviderHealthService, IdentityDbContext>();

        return services;
    }
}
