using CQ.AuthProvider.BusinessLogic.Abstractions.Accounts;
using CQ.AuthProvider.BusinessLogic.Abstractions.Apps;
using CQ.AuthProvider.BusinessLogic.Abstractions.Invitations;
using CQ.AuthProvider.BusinessLogic.Abstractions.Permissions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Roles;
using CQ.AuthProvider.BusinessLogic.Abstractions.Sessions;
using CQ.AuthProvider.BusinessLogic.Abstractions.Tokens;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.BusinessLogic.Abstractions.AppConfig;

public static class ServicesConfig
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services)
    {
        services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IAccountInternalService, AccountService>()

            .AddScoped<ISessionService, SessionService>()
            .AddScoped<ISessionInternalService, SessionService>()

            .AddScoped<IRoleService, RoleService>()
            .AddScoped<IRoleInternalService, RoleService>()

            .AddScoped<IPermissionService, PermissionService>()
            .AddScoped<IPermissionInternalService, PermissionService>()

            .AddScoped<IAppService, AppService>()
            .AddScoped<IAppInternalService, AppService>()

            .AddScoped<ITokenService, GuidTokenService>()

            .AddScoped<IInvitationService, InvitationService>()
            ;

        return services;
    }
}
