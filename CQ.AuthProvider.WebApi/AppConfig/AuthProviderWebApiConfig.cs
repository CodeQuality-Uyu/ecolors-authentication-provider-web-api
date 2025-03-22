using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using AutoMapper;
using CQ.ApiElements.AppConfig;
using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic.Utils;
using CQ.AuthProvider.DataAccess.EfCore.AppConfig;
using CQ.AuthProvider.Postgres.Migrations;
using CQ.AuthProvider.Sql.Migrations;
using CQ.AuthProvider.WebApi.Controllers.Accounts;
using CQ.AuthProvider.WebApi.Controllers.Apps;
using CQ.AuthProvider.WebApi.Controllers.Blobs;
using CQ.AuthProvider.WebApi.Controllers.Invitations;
using CQ.AuthProvider.WebApi.Controllers.Me;
using CQ.AuthProvider.WebApi.Controllers.Permissions;
using CQ.AuthProvider.WebApi.Controllers.Roles;
using CQ.AuthProvider.WebApi.Controllers.Sessions;
using CQ.AuthProvider.WebApi.Controllers.Tenants;
using CQ.AuthProvider.WebApi.Filters;
using CQ.Extensions.Configuration;
using CQ.Extensions.Environments;
using CQ.Extensions.ServiceCollection;
using CQ.IdentityProvider.EfCore.AppConfig;
using CQ.UnitOfWork.EfCore.Core;
using CQ.Utility;
using FluentValidation;
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
        })
            .AddTransient<IValidator<CreateBlobRequest>, CreateBlobRequestValidator>()
            ;

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

            .ConfigureDbContexts(configuration)

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

    private static IServiceCollection ConfigureDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
             .ConfigureAuthDbContext(configuration)
             .ConfigureIdentityDbContext(configuration);

        return services;
    }

    private static IServiceCollection ConfigureAuthDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Auth");
        Guard.ThrowIsNullOrEmpty(connectionString, "ConnectionStrings:Auth");

        var databaseEngine = configuration.GetSection<string>("DatabaseEngine:Auth");

        services = databaseEngine switch
        {
            "Sql" => services.ConfigureAuthProviderSql(connectionString),

            "Postgres" => services.ConfigureAuthProviderPostgres(connectionString),

            _ => throw new InvalidOperationException("Invalid value of DatabaseEngine:Auth")
        };

        return services;
    }

    private static IServiceCollection ConfigureIdentityDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Identity");
        Guard.ThrowIsNullOrEmpty(connectionString, "ConnectionStrings:Identity");

        var databaseEngine = configuration.GetSection<string>("DatabaseEngine:Identity");

        services = databaseEngine switch
        {
            "Sql" => services.ConfigureIdentityProviderSql(connectionString),

            "Postgres" => services.ConfigureIdentityProviderPostgres(connectionString),

            _ => throw new InvalidOperationException("Invalid value of DatabaseEngineIdentity")
        };

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

    public static IServiceCollection ConfigureBlob(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var blobConfiguration = configuration.GetSection<BlobConfiguration>("Blob");

        var credentials = new BasicAWSCredentials(blobConfiguration.AccessToken, blobConfiguration.SecretToken);

        AmazonS3Client client;

        if (blobConfiguration.Fake.IsActive && !environment.IsProd())
        {
            var fakeConfig = new AmazonS3Config
            {
                ServiceURL = blobConfiguration.Fake.ServiceUrl,
                ForcePathStyle = true,
            };

            client = new AmazonS3Client(credentials, fakeConfig);
        }
        else
        {
            var region = RegionEndpoint.GetBySystemName(blobConfiguration.Region);

            client = new AmazonS3Client(credentials, region);
        }

        services
            .AddSingleton<IAmazonS3>(client);

        return services;
    }
}
