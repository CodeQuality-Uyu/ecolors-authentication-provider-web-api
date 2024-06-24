using CQ.Extensions.Configuration;
using CQ.IdentityProvider.EfCore.AppConfig;
using CQ.IdentityProvider.Firebase.AppConfig;
using CQ.IdentityProvider.Mongo.AppConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.IdentityProvider.Factory;

public static class IdentityProviderConfig
{
    public static IServiceCollection ConfigureIdentityProvider(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var identitySection = configuration.GetSection<IdentitySection>(IdentitySection.Name);

        switch (identitySection.Engine)
        {
            case IdentityEngine.Sql:
                {
                    services
                        .AddEfCoreRepositories(configuration);
                    break;
                }

            case IdentityEngine.Mongo:
                {
                    services
                        .AddMongoRepositories(configuration);
                    break;
                }

            case IdentityEngine.Firebase:
                {
                    services
                        .AddFirebaseRepositories(configuration);
                    break;
                }
            default:
                throw new Exception("Engine for Identity not supported");
        }

        return services;
    }
}
