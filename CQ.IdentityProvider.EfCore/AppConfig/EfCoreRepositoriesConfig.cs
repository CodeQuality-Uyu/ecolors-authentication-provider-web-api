using CQ.AuthProvider.BusinessLogic.Identities;
using CQ.Extensions.ServiceCollection;
using CQ.IdentityProvider.EfCore.Identities;
using CQ.UnitOfWork.EfCore.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.IdentityProvider.EfCore.AppConfig;

public static class EfCoreRepositoriesConfig
{
    public static IServiceCollection ConfigureIdentityProvider(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddScoped<IIdentityProviderHealthService, IdentityDbContext>()
            .AddScoped<PasswordHasher<string>>()
            .AddAbstractionRepository<Identity, IIdentityRepository, IdentityRepository>(LifeTime.Scoped)
            ;

        return services;
    }
}
