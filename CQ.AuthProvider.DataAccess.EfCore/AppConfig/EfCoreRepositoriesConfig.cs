
using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Accounts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.BusinessLogic.EfCore.AppConfig;

public static class EfCoreRepositoriesConfig
{
    public static IServiceCollection ConfigureEfCore(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }
}
