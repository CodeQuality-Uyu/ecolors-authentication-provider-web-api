using CQ.AuthProvider.BusinessLogic.AppConfig.Firebase;
using CQ.Utility;
using dotenv.net.Utilities;
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
            var settingsService = new SettingsService();

            services
                .AddSingleton<ISettingsService, SettingsService>()
                .AddAuthService(settingsService)
                .AddTransient<IResetPasswordService, ResetPasswordService>();

            return services;
        }

        private static IServiceCollection AddAuthService(this IServiceCollection services, ISettingsService settingsService)
        {
            var authType = settingsService.GetValue(EnvironmentVariable.AuthType);
            var authService = !string.IsNullOrEmpty(authType) ? new AuthTypeOption(authType) : AuthTypeOption.Firebase;

            if (authService == AuthTypeOption.Firebase)
            {
                services.ConfigFirebaseAuth(settingsService);
            }

            return services;
        }

        private static IServiceCollection ConfigFirebaseAuth(this IServiceCollection services, ISettingsService settingsService)
        {
            services
                .AddFirebase(settingsService)
                .AddTransient<IAuthService, AuthFirebaseService>()
                .AddTransient<HttpClientAdapter>((serviceProvider) =>
            {
                var apiUrl = settingsService.GetValue(EnvironmentVariable.ApiUrl);
                var baseUrl = settingsService.GetValue(EnvironmentVariable.Firebase.ApiUrl);

                var baseHeaders = new List<(string, string)>
                {
                    ("Referer", apiUrl)
                };

                return new HttpClientAdapter(baseUrl, baseHeaders);
            })
                .AddTransient<ISessionService, SessionFirebaseService>();

            return services;
        }

        private static IServiceCollection ConfigMongoAuth(this IServiceCollection services)
        {

            //var connectionToUse = Environment.GetEnvironmentVariable("mongo-db:default");
            //var connectionStringEnvVariable = string.IsNullOrEmpty(connectionToUse) ? "mongo-db:connection-string" : $"mongo-db:{connectionToUse}-connection-string";
            //var connectionString = Environment.GetEnvironmentVariable(connectionStringEnvVariable);
            //var dataBaseName= Environment.GetEnvironmentVariable("mongo-db:database-name");

            //services.AddMongoDatabase(connectionString, dataBaseName);

            //services.AddUnitOfWork(Orms.MONGO_DB);

            return services;
        }
    }
}
