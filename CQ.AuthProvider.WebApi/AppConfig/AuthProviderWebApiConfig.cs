using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.BusinessLogic.Abstractions.AppConfig;
using CQ.AuthProvider.DataAccess.Factory;
using CQ.AuthProvider.WebApi.Filters.Exception;
using CQ.Extensions.ServiceCollection;
using CQ.IdentityProvider.Factory;

namespace CQ.AuthProvider.WebApi.AppConfig;

internal static class AuthProviderWebApiConfig
{
    public static IServiceCollection ConfigureApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddExceptionGlobalHandlerService<CQAuthExceptionRegistryService>(LifeTime.Singleton)

            .ConfigureServices(configuration)

            .ConfigureDataAccess(configuration)

            .ConfigureIdentityProvider(configuration)
            ;

        return services;
    }
}
