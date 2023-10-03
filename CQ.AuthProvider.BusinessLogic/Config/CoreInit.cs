using Amazon.Runtime.Internal;
using CQ.UnitOfWork.Entities;
using CQ.UnitOfWork.Init;
using dotenv.net.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlayerFinder.Auth.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerFinder.Auth.Core.DI
{
    public static class CoreInit
    {
        public static void AddCore(this IServiceCollection services)
        {
            services.AddFirebase();

            var connectionToUse = Environment.GetEnvironmentVariable("mongo-db:default");
            var connectionStringEnvVariable = string.IsNullOrEmpty(connectionToUse) ? "mongo-db:connection-string" : $"mongo-db:{connectionToUse}-connection-string";
            var connectionString = Environment.GetEnvironmentVariable(connectionStringEnvVariable);
            var dataBaseName= Environment.GetEnvironmentVariable("mongo-db:database-name");

            services.AddMongoDatabase(connectionString, dataBaseName);

            services.AddUnitOfWork(Orms.MONGO_DB);

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, FirebaseAuthService>();
            services.AddTransient<IResetPasswordService, ResetPasswordService>();
        }
    }
}
