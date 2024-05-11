using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Accounts.Mappings;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Authorizations.Mappings;
using CQ.AuthProvider.BusinessLogic.ClientSystems;
using CQ.AuthProvider.BusinessLogic.ClientSystems.Mappings;
using CQ.AuthProvider.BusinessLogic.ResetPasswords;
using CQ.AuthProvider.BusinessLogic.Sessions;
using CQ.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public static class AppDependencyInjection
    {
        public static IServiceCollection AddCQServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddAutoMapper(
                typeof(AccountProfile),
                typeof(RoleProfile),
                typeof(PermissionProfile),
                typeof(PermissionKeyProfile),
                typeof(RoleKeyProfile),
                typeof(ClientSystemProfile));

            services.AddScoped<HttpClientAdapter>();

            var connectionString = configuration.GetConnectionString(ConnectionStrings.Auth);

            Guard.ThrowIsNullOrEmpty(connectionString, "ConnectionStrings:Auth");

            var authOptions = configuration
                .GetSection(ConfigOptions.Auth)
                .Get<AuthOptions>();

            Guard.ThrowIsNull(authOptions, "Auth");

            services.AddSingleton<AuthOptions>(authOptions);

            if (authOptions.Engine == DatabaseEngine.Sql)
            {
                services
                    .AddScoped<IAccountService, AccountEfCoreService>()
                    .AddScoped<IRoleService, RoleEfCoreService>()
                    .AddScoped<IRoleInternalService<RoleEfCore>, RoleEfCoreService>()
                    .AddScoped<IRoleInternalService, RoleEfCoreService>()
                    .AddScoped<IPermissionInternalService<PermissionEfCore>, PermissionEfCoreService>()
                    .AddScoped<IPermissionService, PermissionEfCoreService>()
                    .AddScoped<IResetPasswordService, ResetPasswordEfCoreService>()
                    .AddScoped<IClientSystemService, ClientSystemEfCoreService>();
            }

            if (authOptions.Engine == DatabaseEngine.Mongo)
            {
                services
                    .AddScoped<IAccountService, AccountMongoService>()
                    .AddScoped<IRoleService, RoleMongoService>()
                    .AddScoped<IRoleInternalService<RoleMongo>, RoleMongoService>()
                    .AddScoped<IRoleInternalService, RoleMongoService>()
                    .AddScoped<IPermissionInternalService<PermissionMongo>, PermissionMongoService>()
                    .AddScoped<IPermissionService, PermissionMongoService>()
                    .AddScoped<IResetPasswordService, ResetPasswordMongoService>()
                    .AddScoped<IClientSystemService, ClientSystemMongoService>();
            }

            var identity = configuration
                .GetSection(ConfigOptions.Identity)
                .Get<IdentityOptions>();

            Guard.ThrowIsNull(identity, "Identity");

            if (identity.Type == IdentityType.Database)
            {
                services
                    .AddScoped<ISessionService, SessionService>()
                    .AddScoped<ISessionInternalService, SessionService>();
            }

            return services;
        }
    }
}
