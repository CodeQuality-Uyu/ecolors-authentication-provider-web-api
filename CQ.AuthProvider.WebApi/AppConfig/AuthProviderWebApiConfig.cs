using AutoMapper;
using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.DataAccess.EfCore.AppConfig;
using CQ.AuthProvider.WebApi.Controllers.Accounts;
using CQ.AuthProvider.WebApi.Controllers.Apps;
using CQ.AuthProvider.WebApi.Controllers.Invitations;
using CQ.AuthProvider.WebApi.Controllers.Me;
using CQ.AuthProvider.WebApi.Controllers.Permissions;
using CQ.AuthProvider.WebApi.Controllers.Roles;
using CQ.AuthProvider.WebApi.Controllers.Sessions;
using CQ.AuthProvider.WebApi.Controllers.Tenants;
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
            ;

        return services;
    }

    private static IServiceCollection AddMappings(
        this IServiceCollection services)
    {
        services
            .AddAutoMapper(typeof(Program))
            .AddKeyedTransient<IMapper>(MapperKeyedService.Presentation, (provider, _) =>
            {
                var config = new MapperConfiguration(config =>
                {
                    config.ConstructServicesUsing(provider.GetService);

                    config.AddProfile<MeProfile>();
                    config.AddProfile<PermissionProfile>();
                    config.AddProfile<RoleProfile>();
                    config.AddProfile<InvitationProfile>();
                    config.AddProfile<SessionProfile>();
                    config.AddProfile<AppProfile>();
                    config.AddProfile<AccountProfile>();
                    config.AddProfile<TenantProfile>();
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
        if (environment.IsProd())
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
