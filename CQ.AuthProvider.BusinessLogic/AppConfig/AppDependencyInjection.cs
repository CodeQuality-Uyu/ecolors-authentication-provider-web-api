using CQ.AuthProvider.BusinessLogic.Accounts;
using CQ.AuthProvider.BusinessLogic.Accounts.Mappings;
using CQ.AuthProvider.BusinessLogic.Authorizations;
using CQ.AuthProvider.BusinessLogic.Authorizations.Mappings;
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
                typeof(PermissionKeyProfile));

            var mongoConnectionString = configuration.GetConnectionString("AuthMongo");
            var sqlConnectionString = configuration.GetConnectionString("AuthSql");


            if(Guard.IsNotNullOrEmpty(sqlConnectionString)) 
            {
                services
                    .AddSingleton<IAccountService, AccountEfCoreService>()
                    .AddSingleton<IRoleService, RoleEfCoreService>()
                    .AddSingleton<IRoleInternalService<RoleEfCore>, RoleEfCoreService>()
                    .AddSingleton<IPermissionInternalService, PermissionEfCoreService>()
                    .AddSingleton<IPermissionService, PermissionEfCoreService>()
                    .AddSingleton<IResetPasswordService, ResetPasswordEfCoreService>();
            }
            
            if(Guard.IsNotNullOrEmpty(mongoConnectionString)) 
            {
                services
                    .AddSingleton<IAccountService, AccountMongoService>()
                    .AddSingleton<IRoleService, RoleMongoService>()
                    .AddSingleton<IRoleInternalService<RoleMongo>, RoleMongoService>()
                    .AddSingleton<IPermissionInternalService, PermissionMongoService>()
                    .AddSingleton<IPermissionService, PermissionMongoService>()
                    .AddSingleton<IResetPasswordService, ResetPasswordMongoService>();
            }

            var identity = configuration
                .GetSection(IdentityOptions.Identity)
                .Get<IdentityOptions>();

            Guard.ThrowIsNull(identity, nameof(identity));

            if (identity.Type == "Database")
            {
                services.AddSingleton<ISessionService, SessionService>();
            }

            return services;
        }
    }
}
