using CQ.AuthProvider.BusinessLogic.AppConfig;
using CQ.AuthProvider.BusinessLogic;
using CQ.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.Firebase.AppConfig
{
    public static class FirebaseDependencyInjection
    {
        public static IServiceCollection AddFirebaseServices(this IServiceCollection services, ISettingsService settingsService)
        {
            services
                .AddFirebase(settingsService)
                .AddSingleton<IIdentityProviderRepository, AuthRepository>()
                .AddSingleton<IIdentityProviderHealthService, AuthRepository>()
                .AddTransient<HttpClientAdapter>((serviceProvider) =>
                {
                    var apiUrl = settingsService.GetValue(EnvironmentVariable.Firebase.RefererApiUrl);
                    var baseUrl = settingsService.GetValue(EnvironmentVariable.Firebase.ApiUrl);

                    var baseHeaders = new List<Header>
                {
                    new ("Referer", apiUrl)
                };

                    return new HttpClientAdapter(baseUrl, baseHeaders);
                })
                .AddSingleton<ISessionService, SessionService>();


            return services;
        }
    }
}
