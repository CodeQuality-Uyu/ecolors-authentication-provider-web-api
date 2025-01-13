using CQ.AuthProvider.DataAccess.EfCore;
using CQ.Extensions.ServiceCollection;
using CQ.IdentityProvider.EfCore;
using CQ.UnitOfWork.EfCore.Configuration;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.Postgres.Migrations;

public static class ConfigPostgres
{
    public static IServiceCollection ConfigureAuthProviderPostgres(
        this IServiceCollection services,
        string connectionString)
    {
        return services.ConfigurePostgres<AuthDbContext>(connectionString);
    }

    public static IServiceCollection ConfigureIdentityProviderPostgres(
        this IServiceCollection services,
        string connectionString)
    {
        return services.ConfigurePostgres<IdentityDbContext>(connectionString);
    }

    private static IServiceCollection ConfigurePostgres<TDbContext>(
        this IServiceCollection services,
        string connectionString)
        where TDbContext : EfCoreContext
    {
        services
            .AddContext<TDbContext>(options =>
            options.UseNpgsql(connectionString),
            LifeTime.Scoped);

        return services;
    }
}
