using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.ResetPasswords;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Accounts.Mappings;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Permissions.Mappings;
using CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Sessions;
using CQ.Extensions.ServiceCollection;
using CQ.UnitOfWork.EfCore.Configuration;
using CQ.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.DataAccess.EfCore.AppConfig;

public static class EfCoreRepositoriesConfig
{
    public static IServiceCollection AddEfCoreRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddMappings()
            .AddRepositories(configuration)
            ;

        return services;
    }

    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        services
            .AddAutoMapper(
            typeof(PermissionProfile),
            typeof(AccountProfile));

        return services;
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Auth");

        Guard.ThrowIsNullOrEmpty(connectionString, "ConnectionStrings:Auth");

        services
            .AddContext<AuthDbContext>(options =>
            options
            .UseSqlServer(connectionString),
            LifeTime.Scoped)

            .AddAbstractionRepository<AccountEfCore, IAccountRepository, AccountRepository>(LifeTime.Scoped)
            .AddAbstractionRepository<RoleEfCore, IRoleRepository, RoleRepository>(LifeTime.Scoped)
            .AddRepositoryForContext<RolePermission, AuthDbContext>(LifeTime.Scoped)
            .AddAbstractionRepository<PermissionEfCore, IPermissionRepository, PermissionRepository>(LifeTime.Scoped)
            .AddAbstractionRepository<SessionEfCore, ISessionRepository, SessionRepository>(LifeTime.Scoped)
            .AddAbstractionRepository<ResetPasswordEfCore, IResetPasswordRepository, ResetPasswordRepository>(LifeTime.Scoped)
            ;

        return services;
    }
}
