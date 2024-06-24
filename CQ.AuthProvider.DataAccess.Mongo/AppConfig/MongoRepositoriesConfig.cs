

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.Mongo.AppConfig;

public static class MongoRepositoriesConfig
{
    public static IServiceCollection AddMongoRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}
