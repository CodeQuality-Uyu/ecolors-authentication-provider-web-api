using CQ.AuthProvider.DataAccess.EfCore.AppConfig;
using CQ.Utility;
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
            var authOptions = configuration
                .GetSection(AuthSection.Name)
                .Get<AuthSection>();

            Guard.ThrowIsNull(authOptions, "Auth");

            switch (authOptions.Engine)
            {
                case DatabaseEngine.Sql:
                    {
                        services
                            .ConfigureEfCore(configuration);

                        break;
                    }
                case DatabaseEngine.Mongo:
                    {
                        services
                            .ConfigureMongo(configuration);

                        break;
                    }

                default:
                    throw new Exception();
            }


            return services;
        }
    }
}
