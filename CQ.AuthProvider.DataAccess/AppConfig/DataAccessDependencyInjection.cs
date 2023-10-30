using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.Firebase.AppConfig;
using CQ.AuthProvider.Mongo.AppConfig;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.DataAccess.AppConfig
{
    public static class DataAccessDependencyInjection
    {
        public static IServiceCollection AddCQDataAccess(this IServiceCollection services)
        {
            var settingsService = new SettingsService();

            var mongoConnection = settingsService.GetValueOrDefault(EnvironmentVariable.Mongo.ConnectionString);

            if (!string.IsNullOrEmpty(mongoConnection))
            {
                services.AddMongoServices(settingsService, mongoConnection);

                return services;
            }


            settingsService.GetValue(EnvironmentVariable.Firebase.ProjectId);

            services.AddFirebaseServices(settingsService);

            return services;
        }
    }
}
