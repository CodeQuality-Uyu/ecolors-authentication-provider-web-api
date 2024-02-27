using CQ.AuthProvider.BusinessLogic.Accounts;
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
                .AddSingleton<IAccountService, AccountService>()
                .AddSingleton<IRoleService, RoleService>()
                .AddSingleton<IRoleInternalService, RoleService>()
                .AddSingleton<IPermissionInternalService, PermissionService>()
                .AddSingleton<IPermissionService, PermissionService>()
                .AddSingleton<IResetPasswordService, ResetPasswordService>();

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
