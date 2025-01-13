using CQ.AuthProvider.DataAccess.EfCore;
using CQ.Extensions.ServiceCollection;
using CQ.IdentityProvider.EfCore;
using CQ.UnitOfWork.EfCore.Configuration;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.Sql.Migrations;

public static class ConfigSqlExtensions
{
    public static IServiceCollection ConfigureAuthProviderSql(
        this IServiceCollection services,
        string connectionString)
    {
        return services.ConfigureSql<AuthDbContext>(connectionString);
    }

    public static IServiceCollection ConfigureIdentityProviderSql(
        this IServiceCollection services,
        string connectionString)
    {
        return services.ConfigureSql<IdentityDbContext>(connectionString);
    }

    private static IServiceCollection ConfigureSql<TDbContext>(
        this IServiceCollection services,
        string connectionString)
        where TDbContext : EfCoreContext
    {
        services
            .AddContext<TDbContext>(options =>
            options.UseSqlServer(connectionString),
            LifeTime.Scoped);

        return services;
    }
}
