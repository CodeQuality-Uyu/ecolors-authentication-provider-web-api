using CQ.AuthProvider.DataAccess.EfCore.AppConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.Factory
{
    public static class DataAccessConfig
    {
        public static IServiceCollection ConfigureDataAccess(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services
                .ConfigureEfCore(configuration);

            return services;
        }
    }
}
