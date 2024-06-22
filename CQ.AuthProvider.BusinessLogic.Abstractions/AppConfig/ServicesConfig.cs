using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.AppConfig;

public static class ServicesConfig
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IAccountInternalService, AccountService>()
            ;

        return services;
    }
}
