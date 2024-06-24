using CQ.AuthProvider.DataAccess.EfCore.AppConfig;
using CQ.AuthProvider.DataAccess.Mongo.AppConfig;
using CQ.Extensions.Configuration;
using CQ.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.Factory;

public static class DataAccessConfig
{
    public static IServiceCollection ConfigureDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authOptions = configuration.GetSection<AuthSection>(AuthSection.Name);

        Guard.ThrowIsNull(authOptions, "Auth");

        switch (authOptions.Engine)
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
