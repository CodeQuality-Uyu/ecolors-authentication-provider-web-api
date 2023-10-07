using CQ.AuthProvider.BusinessLogic.Config.Firebase;
using dotenv.net.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.BusinessLogic.Config
{
    public static class CoreInit
    {
        public static void AddCQServices(this IServiceCollection services)
        {
            AddAuthService(services);

            //var connectionToUse = Environment.GetEnvironmentVariable("mongo-db:default");
            //var connectionStringEnvVariable = string.IsNullOrEmpty(connectionToUse) ? "mongo-db:connection-string" : $"mongo-db:{connectionToUse}-connection-string";
            //var connectionString = Environment.GetEnvironmentVariable(connectionStringEnvVariable);
            //var dataBaseName= Environment.GetEnvironmentVariable("mongo-db:database-name");

            //services.AddMongoDatabase(connectionString, dataBaseName);

            //services.AddUnitOfWork(Orms.MONGO_DB);

            services.AddTransient<IResetPasswordService, ResetPasswordService>();
        }

        private static void AddAuthService(IServiceCollection services)
        {
            var authService = Environment.GetEnvironmentVariable("auth-service") ?? AuthServiceOptions.Firebase;

            if (authService == AuthServiceOptions.Firebase)
            {
                ConfigFirebaseAuth(services);
            }
        }

        private static void ConfigFirebaseAuth(this IServiceCollection services)
        {
            services.AddFirebase();
            services.AddTransient<IAuthService, FirebaseAuthService>();
        }
    }
}
