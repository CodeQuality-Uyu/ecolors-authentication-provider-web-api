using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Apps;
using CQ.AuthProvider.BusinessLogic.Blobs;
using CQ.AuthProvider.BusinessLogic.Emails;
using CQ.AuthProvider.BusinessLogic.Invitations;
using CQ.AuthProvider.BusinessLogic.Me;
using CQ.AuthProvider.BusinessLogic.Permissions;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.BusinessLogic.Roles;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.BusinessLogic.Tenants;
using CQ.AuthProvider.BusinessLogic.Tokens;
using CQ.Extensions.ServiceCollection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.BusinessLogic.AppConfig;

public static class ServicesConfig
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services)
    {
        services
            .AddServices()

            .AddEmailServices()

            .AddTokenService<GuidTokenService>(LifeTime.Transient)

            .AddItemLoggedService<SessionService>(LifeTime.Scoped)

            .AddValidators();

        return services;
    }

    private static IServiceCollection AddServices(
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

            .AddScoped<IInvitationService, InvitationService>()

            .AddScoped<IResetPasswordService, ResetPasswordService>()

            .AddScoped<IMeService, MeService>()

            .AddScoped<ITenantService, TenantService>()
            .AddScoped<ITenantInternalService, TenantService>()
            ;

        return services;
    }

    private static IServiceCollection AddEmailServices(this IServiceCollection services)
    {
        services
            .AddTransient<IEmailService, EmailService>();
        
        return services;
    }

    private static IServiceCollection AddValidators(
        this IServiceCollection services)
    {
        services
            .AddTransient<IValidator<CreateAppArgs>, CreateAppArgsValidator>()
            .AddTransient<IValidator<CreatePermissionArgs>, CreatePermissionArgsValidator>()
            .AddTransient<IValidator<CreateBulkPermissionArgs>, CreateBulkPermissionArgsValidator>()
            .AddTransient<IValidator<CreateRoleArgs>, CreateRoleArgsValidator>()
            .AddTransient<IValidator<CreateBulkRoleArgs>, CreateBulkRoleArgsValidator>()
            .AddTransient<IValidator<CreateAccountArgs>, CreateAccountArgsValidator>()
            .AddTransient<IValidator<CreateAccountForArgs>, CreateAccountForArgsValidator>()
            .AddTransient<IValidator<CreateInvitationArgs>, CreateInvitationArgsValidator>()
            .AddTransient<IValidator<UpdatePasswordArgs>, UpdatePasswordArgsValidator>()
            .AddTransient<IValidator<CreateSessionCredentialsArgs>, CreateSessionCredentialsArgsValidator>()
            .AddTransient<IValidator<AcceptResetPasswordArgs>, AcceptResetPasswordArgsValidator>()
            .AddTransient<IValidator<CreateResetPasswordArgs>, CreateResetPasswordArgsValidator>()
            .AddTransient<IValidator<CreateTenantArgs>, CreateTenantArgsValidator>()
            .AddTransient<IValidator<UpdateRolesArgs>, UpdateRolesArgsValidator>()
            ;

        return services;
    }
}