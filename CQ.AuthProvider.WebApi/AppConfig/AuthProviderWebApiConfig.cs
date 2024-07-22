using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.AppConfig;
using CQ.AuthProvider.DataAccess.Factory;
using CQ.AuthProvider.WebApi.Filters.Exception;
using CQ.Extensions.Configuration;
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
            
            .AddMappings()

            .ConfigureServices()

            .ConfigureDataAccess(configuration)

            .ConfigureIdentityProvider(configuration)

            .AddFakeAuthentication(configuration)
            ;

        return services;
    }

    private static IServiceCollection AddMappings(
        this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(Program));

        return services;
    }

    private static IServiceCollection AddFakeAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var isFakeAccountActive = Convert.ToBoolean(configuration["Auth:Fake:IsActive"]);

        if (!isFakeAccountActive)
        {
            return services;
        }

        var fakeAccount = configuration.GetSection<FakeAccountLogged>("Auth:Fake");
        var accountLogged = fakeAccount.Build();

        services.AddSingleton<AccountLogged>(accountLogged);

        return services;
    }
}
