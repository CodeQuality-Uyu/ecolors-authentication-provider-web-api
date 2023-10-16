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
    public static class CoreInit
    {
        public static IServiceCollection AddCQServices(this IServiceCollection services)
        {
            services
                .AddAuthService()
                .AddTransient<IResetPasswordService, ResetPasswordService>();

            return services;
        }

        private static IServiceCollection AddAuthService(this IServiceCollection services)
        {
            var authService = Environment.GetEnvironmentVariable("auth-service") ?? AuthServiceOptions.Firebase;

            if (authService == AuthServiceOptions.Firebase)
            {
                services.ConfigFirebaseAuth();
            }

            return services;
        }

        private static IServiceCollection ConfigFirebaseAuth(this IServiceCollection services)
        {
            services
                .AddFirebase()
                .AddTransient<IAuthService, AuthFirebaseService>()
                .AddTransient((serviceProvider) =>
            {
                return new HttpClientAdapter("https://identitytoolkit.googleapis.com/v1");
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
