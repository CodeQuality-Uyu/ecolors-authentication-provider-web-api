using CQ.Extensions.ServiceCollection;
using CQ.UnitOfWork.MongoDriver.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CQ.AuthProvider.DataAccess.Mongo.AppConfig;

public static class MongoRepositoriesConfig
{
    public static IServiceCollection AddMongoRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Auth");

        services
            .AddContext<AuthDbContext>(
            new MongoClient(connectionString),
            LifeTime.Scoped)
            ;

        return services;
    }
}
