using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.DataAccess.EfCore.AppConfig;
using CQ.AuthProvider.WebApi.Controllers.Permissions.Mappings;
using CQ.AuthProvider.WebApi.Controllers.Roles.Mappings;
using CQ.AuthProvider.WebApi.Filters.Exception;
using CQ.Extensions.Environments;
using CQ.Extensions.ServiceCollection;
using CQ.IdentityProvider.EfCore.AppConfig;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;

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

            .AddFakeAuthentication<FakeAccountLogged>(configuration)
            ;

        return services;
    }

    private static IServiceCollection AddMappings(
        this IServiceCollection services)
    {
        services
            .AddAutoMapper(config =>
            {
                config.AddProfile<PermissionProfile>();
                config.AddProfile<RoleProfile>();
            });

        return services;
    }

    public static IServiceProvider AddDbContextMissingMigrations<TContext>(
        this IServiceProvider services,
        IWebHostEnvironment environment)
        where TContext : EfCoreContext
    {
        if (!environment.IsLocal())
        {
            return services;
        }

        using var scope = services.CreateScope();

        var authProviderDbContext = scope.ServiceProvider.GetRequiredService<TContext>();

        var pendingMigrations = authProviderDbContext.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
        {
            authProviderDbContext.Database.Migrate();
        }

        return services;
    }
}
