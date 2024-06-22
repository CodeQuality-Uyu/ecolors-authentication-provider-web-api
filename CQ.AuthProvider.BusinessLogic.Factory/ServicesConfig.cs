using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.BusinessLogic.Factory;
public static class ServicesConfig
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authOptions = configuration
                .GetSection(ConfigOptions.Name)
                .Get<AuthOptions>();

        if(authOptions.Engine == DatabaseEngine.Sql)
        {
            services
                .ConfigureSqlServices(configuration);
        }
        else
        {
            services
                .ConfigureMongoServices(configuration);
        }

        return services;
    }
}
