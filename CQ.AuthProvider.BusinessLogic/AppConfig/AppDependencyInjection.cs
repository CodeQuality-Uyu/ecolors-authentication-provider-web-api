using CQ.Utility;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.AppConfig
{
    public static class AppDependencyInjection
    {
        public static IServiceCollection AddCQServices(this IServiceCollection services)
        {
            services
                .AddSingleton<ISettingsService, SettingsService>()
                .AddSingleton<IAuthService, AuthService>()
                .AddSingleton<IRoleService, RoleService>()
                .AddSingleton<IRoleInternalService, RoleService>()
                .AddSingleton<IPermissionInternalService, PermissionService>()
                .AddSingleton<IPermissionService, PermissionService>()
                .AddSingleton<IResetPasswordService, ResetPasswordService>();

            return services;
        }
    }
}
