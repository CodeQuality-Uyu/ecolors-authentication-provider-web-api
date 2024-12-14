using AutoMapper;
using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.AuthProvider.BusinessLogic.Tokens;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.DataAccess.EfCore.AppConfig;
using CQ.AuthProvider.WebApi.Controllers.Accounts.Mappings;
using CQ.AuthProvider.WebApi.Controllers.Apps.Mappings;
using CQ.AuthProvider.WebApi.Controllers.Invitations.Mappings;
using CQ.AuthProvider.WebApi.Controllers.Permissions.Mappings;
using CQ.AuthProvider.WebApi.Controllers.Roles.Mappings;
using CQ.AuthProvider.WebApi.Controllers.Sessions.Mappings;
using CQ.AuthProvider.WebApi.Filters;
using CQ.Extensions.Environments;
using CQ.Extensions.ServiceCollection;
using CQ.IdentityProvider.EfCore.AppConfig;
using CQ.UnitOfWork.EfCore.Core;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace CQ.AuthProvider.WebApi.AppConfig;

internal static class AuthProviderWebApiConfig
{
    public static IServiceCollection ConfigureAutoValidation(
        this IServiceCollection services)
    {
        services
            .AddFluentValidationAutoValidation(configuration =>
        {
            // Disable the built-in .NET model (data annotations) validation.
            configuration.DisableBuiltInModelValidation = true;

            // Only validate controllers decorated with the `FluentValidationAutoValidation` attribute.
            //configuration.ValidationStrategy = ValidationStrategy.Annotations;

            // Enable validation for parameters bound from `BindingSource.Body` binding sources.
            //configuration.EnableBodyBindingSourceAutomaticValidation = true;

            // Enable validation for parameters bound from `BindingSource.Form` binding sources.
            configuration.EnableFormBindingSourceAutomaticValidation = true;

            // Enable validation for parameters bound from `BindingSource.Query` binding sources.
            //configuration.EnableQueryBindingSourceAutomaticValidation = true;

            // Enable validation for parameters bound from `BindingSource.Path` binding sources.
            configuration.EnablePathBindingSourceAutomaticValidation = true;

            // Enable validation for parameters bound from 'BindingSource.Custom' binding sources.
            configuration.EnableCustomBindingSourceAutomaticValidation = true;

            // Replace the default result factory with a custom implementation.
            configuration.OverrideDefaultResultFactoryWith<CQBadRequestResponseFactory>();
        });

        return services;
    }

    public static IServiceCollection ConfigureApiServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services
            .AddExceptionGlobalHandlerService<CQAuthExceptionRegistryService>(LifeTime.Transient)

            .AddMappings()

            .ConfigureServices()

            .ConfigureDataAccess(configuration)

            .ConfigureIdentityProvider(configuration)

            .AddFakeAuthentication<FakeAccountLogged>(configuration, environment, fakeAuthenticationLifeTime: LifeTime.Transient)

            .AddTokenService<GuidTokenService>(LifeTime.Transient)

            .AddItemLoggedService<SessionService>(LifeTime.Scoped)
            ;

        return services;
    }

    private static IServiceCollection AddMappings(
        this IServiceCollection services)
    {
        services
            .AddKeyedTransient<IMapper>(MapperKeyedService.Presentation, (provider, _) =>
            {
                var config = new MapperConfiguration(config =>
                {
                    config.AddProfile<PermissionProfile>();
                    config.AddProfile<RoleProfile>();
                    config.AddProfile<InvitationProfile>();
                    config.AddProfile<SessionProfile>();
                    config.AddProfile<AppProfile>();
                    config.AddProfile<AccountProfile>();
                });

                return config.CreateMapper();
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
