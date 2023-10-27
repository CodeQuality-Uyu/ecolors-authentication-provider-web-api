using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic;
using CQ.Utility;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.Firebase.AppConfig
{
    public static class FirebaseDependencyInjection
    {
        public static IServiceCollection AddFirebaseServices(this IServiceCollection services, ISettingsService settingsService)
        {
            services
                .AddFirebase(settingsService)
                .AddSingleton<IAuthRepository, AuthRepository>()
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
                .AddSingleton<ISessionRepository, SessionRepository>();

            return services;
        }
    }
}
