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

            var mongoConnectionString = configuration.GetConnectionString("AuthMongo");
            var sqlConnectionString = configuration.GetConnectionString("AuthSql");

            if(Guard.IsNotNullOrEmpty(sqlConnectionString)) 
                services
                    .AddScoped<IAccountService, AccountEfCoreService>()
                    .AddScoped<IRoleService, RoleEfCoreService>()
                    .AddScoped<IRoleInternalService<RoleEfCore>, RoleEfCoreService>()
                    .AddScoped<IPermissionInternalService<PermissionEfCore>, PermissionEfCoreService>()
                    .AddScoped<IPermissionService, PermissionEfCoreService>()
                    .AddScoped<IResetPasswordService, ResetPasswordEfCoreService>()
                    .AddScoped<IClientSystemService, ClientSystemEfCoreService>();
            
            if(Guard.IsNotNullOrEmpty(mongoConnectionString)) 
                services
                    .AddScoped<IAccountService, AccountMongoService>()
                    .AddScoped<IRoleService, RoleMongoService>()
                    .AddScoped<IRoleInternalService<RoleMongo>, RoleMongoService>()
                    .AddScoped<IPermissionInternalService<PermissionMongo>, PermissionMongoService>()
                    .AddScoped<IPermissionService, PermissionMongoService>()
                    .AddScoped<IResetPasswordService, ResetPasswordMongoService>()
                    .AddScoped<IClientSystemService, ClientSystemMongoService>();

            if (Guard.IsNullOrEmpty(mongoConnectionString) && Guard.IsNullOrEmpty(sqlConnectionString))
                throw new Exception("Missing Auth connection string: AuthMongo or AuthSql");

            var identity = configuration
                .GetSection(IdentityOptions.Identity)
                .Get<IdentityOptions>();

            Guard.ThrowIsNull(identity, nameof(identity));

            if (identity.Type == IdentityType.Database)
                services.AddScoped<ISessionService, SessionService>();

            return services;
        }
    }
}
