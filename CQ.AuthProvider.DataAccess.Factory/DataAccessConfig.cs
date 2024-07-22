using CQ.AuthProvider.DataAccess.EfCore.AppConfig;
using CQ.AuthProvider.DataAccess.Mongo.AppConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.Factory;

public static class DataAccessConfig
{
    public static IServiceCollection ConfigureDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authEngine = Enum.Parse<AuthEngine>(configuration["Auth:Engine"]!);

        switch (authEngine)
        {
            case AuthEngine.Sql:
                {
                    services
                        .AddEfCoreRepositories(configuration);

                    break;
                }
            case AuthEngine.Mongo:
                {
                    services
                        .AddMongoRepositories(configuration);

                    break;
                }

            default:
                throw new Exception("Engine for Auth not supported");
        }


        return services;
    }
}
