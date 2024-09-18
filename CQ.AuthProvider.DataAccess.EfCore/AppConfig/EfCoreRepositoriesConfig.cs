using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.DataAccess.EfCore.Accounts;
using CQ.AuthProvider.DataAccess.EfCore.Apps;
using CQ.AuthProvider.DataAccess.EfCore.Apps.Mappings;
using CQ.AuthProvider.DataAccess.EfCore.Invitations;
using CQ.AuthProvider.DataAccess.EfCore.Invitations.Mappings;
using CQ.AuthProvider.DataAccess.EfCore.Permissions;
using CQ.AuthProvider.DataAccess.EfCore.Permissions.Mappings;
using CQ.AuthProvider.DataAccess.EfCore.ResetPasswords;
using CQ.AuthProvider.DataAccess.EfCore.Roles;
using CQ.AuthProvider.DataAccess.EfCore.Roles.Mappings;
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
    public static IServiceCollection ConfigureDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddMappings()
            .AddRepositories(configuration)
            ;

        return services;
    }

    private static IServiceCollection AddMappings(this IServiceCollection services)
    {
        services
            .AddAutoMapper(config =>
            {
                config.DisableConstructorMapping();

                config.AddProfile<PermissionProfile>();
                config.AddProfile<RoleProfile>();
                config.AddProfile<InvitationProfile>();
                config.AddProfile<AppProfile>();
            });

        return services;
    }

    private static IServiceCollection AddRepositories(
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
            .AddAbstractionRepository<InvitationEfCore, IInvitationRepository, InvitationRepository>(LifeTime.Scoped)
            .AddAbstractionRepository<AppEfCore, IAppRepository, AppRepository>(LifeTime.Scoped)
            ;

        return services;
    }
}
