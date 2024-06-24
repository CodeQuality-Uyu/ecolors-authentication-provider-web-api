using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.IdentityProvider.Factory;

public static class IdentityProviderConfig
{
    public static IServiceCollection ConfigureIdentityProvider(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}
